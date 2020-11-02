using ET;
using System;
using System.Collections.Generic;

namespace Logic
{
	[MessageHandler]
	public class C2G_LoginGateHandler : AMRpcHandler<C2G_LoginGate, G2C_LoginGate>
	{
		protected override async ETTask Run(Session session, C2G_LoginGate request, G2C_LoginGate response, Action reply)
		{
			Scene scene = Game.Scene.Get(request.GateId);
			if (scene == null)
			{
				return;
			}
			
			string account = scene.GetComponent<GateSessionKeyComponent>().Get(request.Key);
			if (account == null)
			{
				response.Error = ErrorCode.ERR_ConnectGateKeyError;
				response.Message = "Gate key验证失败!";
				reply();
				return;
			}
			//key使用后过期
			scene.GetComponent<GateSessionKeyComponent>().Remove(request.Key);
			//数据库操作对象
			DBComponent dbProxyComponent = scene.GetComponent<DBComponent>();

			//查询账号是否存在
			List<Player> result = await dbProxyComponent.Query<Player>(_account => _account.Account == account);
			Player player = null;
			if (result.Count <= 0)
			{
				player = EntityFactory.Create<Player, string>(Game.Scene, account);
				await dbProxyComponent.Save(player);
			}
			else
            {
				player = result[0];

			}
			scene.GetComponent<PlayerComponent>().Add(player);
			session.AddComponent<SessionPlayerComponent>().Player = player;
			session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);

			response.PlayerId = player.Id;
			reply();
			await ETTask.CompletedTask;
		}
	}
}