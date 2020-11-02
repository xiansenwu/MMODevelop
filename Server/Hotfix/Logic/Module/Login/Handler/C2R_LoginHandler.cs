using ET;
using System;
using System.Collections.Generic;
using System.Net;


namespace Logic
{
	[MessageHandler]
	public class C2R_LoginHandler : AMRpcHandler<C2R_Login, R2C_Login>
	{
		protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response, Action reply)
		{
			// 随机分配一个Gate
			StartSceneConfig config = RealmGateAddressHelper.GetGate(session.DomainZone());
			//数据库操作对象
			DBComponent dbProxyComponent = session.DomainScene().GetComponent<DBComponent>();
			if(request.Account.Length <= 0)
            {
				response.Error = ErrorCode.ERR_AccountOrPasswordError;
				reply();
				return;
			}
			//查询账号是否存在
			List<AccountInfo> result = await dbProxyComponent.Query<AccountInfo>(_account => _account.Account == request.Account);

			if (result.Count > 0)
			{
				if (result[0].Password != request.Password)
                {
					response.Error = ErrorCode.ERR_AccountOrPasswordError;
					reply();
					return;
				}
			}
			else
            {
				// 向gate请求一个key,客户端可以拿着这个key连接gate
				G2R_Register _G2R_Register = (G2R_Register)await ActorMessageSenderComponent.Instance.Call(
				config.SceneId, new R2G_Register() { Account = request.Account,Password = request.Password });
				result = await dbProxyComponent.Query<AccountInfo>(_account => _account.Account == request.Account);
			}
			if (result.Count <= 0)
            {
				response.Error = ErrorCode.ERR_AccountOrPasswordError;
				reply();
				return;
			}

			//Log.Debug($"gate address: {MongoHelper.ToJson(config)}");

			// 向gate请求一个key,客户端可以拿着这个key连接gate
			G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey) await ActorMessageSenderComponent.Instance.Call(
				config.SceneId, new R2G_GetLoginKey() {Account = request.Account });

			response.Address = config.OuterAddress;
			response.Key = g2RGetLoginKey.Key;
			response.GateId = g2RGetLoginKey.GateId;
			reply();
		}
	}
}