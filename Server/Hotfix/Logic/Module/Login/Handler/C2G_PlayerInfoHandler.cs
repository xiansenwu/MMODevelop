using System;


namespace ET
{
	// 用来测试消息包含复杂类型，是否产生gc
	[MessageHandler]
	public class C2G_PlayerInfoHandler : AMRpcHandler<C2G_PlayerInfo, G2C_PlayerInfo>
	{
		protected override async ETTask Run(Session session, C2G_PlayerInfo request, G2C_PlayerInfo response, Action reply)
		{

			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			if(player == null)
            {
				response.Error = ErrorCode.ERR_PlayerError;
				response.Message = "player error!";
				reply();
				return;
			}
			//数据库操作对象
			DBComponent dbProxyComponent = session.DomainScene().GetComponent<DBComponent>();

			PlayerInfo _PlayerInfo = player.GetComponent<PlayerInfo>();
			if(_PlayerInfo == null)
            {
				_PlayerInfo = await dbProxyComponent.Query<PlayerInfo>(player.Id);
				if(_PlayerInfo == null)
                {
					
					_PlayerInfo = EntityFactory.CreateWithId<PlayerInfo>(player,player.Id);
					_PlayerInfo.NickName = "he";
					_PlayerInfo.Level = 1;
					_PlayerInfo.Goldens = 1000;
					_PlayerInfo.Diamods = 100;
					await dbProxyComponent.Save(_PlayerInfo);
				}
				player.AddComponent(_PlayerInfo);
			}
			response.PlayerInfo = _PlayerInfo.ToMsgPlayerInfo();

			reply();
			await ETTask.CompletedTask;
		}
	}
}
