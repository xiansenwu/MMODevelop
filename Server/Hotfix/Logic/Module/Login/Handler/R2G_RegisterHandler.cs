using ET;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
	[ActorMessageHandler]
	public class R2G_RegisterHandler : AMActorRpcHandler<Scene, R2G_Register, G2R_Register>
	{
		protected override async ETTask Run(Scene scene, R2G_Register request, G2R_Register response, Action reply)
		{
			//数据库操作对象
			DBComponent dbProxyComponent = scene.GetComponent<DBComponent>();

			//查询账号是否存在
			List<AccountInfo> result = await dbProxyComponent.Query<AccountInfo>(_account => _account.Account == request.Account);
			if (result.Count <= 0)
			{
				AccountInfo newAccount = EntityFactory.CreateWithId<AccountInfo>(scene,IdGenerater.GenerateId());
				newAccount.Account = request.Account;
				newAccount.Password = request.Password;

				await dbProxyComponent.Save(newAccount);
			}
			else
            {
				response.Error = ErrorCode.ERR_AccountAlreadyRegister;
				reply();
				return;
			}
			reply();
			await ETTask.CompletedTask;
		}
	}
}
