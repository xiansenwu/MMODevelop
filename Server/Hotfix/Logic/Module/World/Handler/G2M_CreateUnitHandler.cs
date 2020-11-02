using System;
using UnityEngine;

namespace ET
{
	[ActorMessageHandler]
	public class G2M_CreateUnitHandler : AMActorRpcHandler<Scene, G2M_CreateUnit, M2G_CreateUnit>
	{
		protected override async ETTask Run(Scene scene, G2M_CreateUnit request, M2G_CreateUnit response, Action reply)
		{
			Unit unit = EntityFactory.CreateWithId<Unit>(scene, IdGenerater.GenerateId());
			unit.AddComponent<MoveComponent>();
			unit.AddComponent<UnitPathComponent>();
			unit.Position = new Vector3(-10, 0, -10);

			unit.AddComponent<MailBoxComponent>();
			await unit.AddLocation();
			unit.AddComponent<UnitGateComponent, long>(request.GateSessionId);
			scene.GetComponent<UnitComponent>().Add(unit);
			response.mUnit = new UnitInfo();
			response.mUnit.UnitId = unit.Id;
			response.mUnit.X = unit.Position.x;
			response.mUnit.Y = unit.Position.y;
			response.mUnit.Z = unit.Position.z;

			// 广播创建的unit
			M2C_CreateUnits createUnits = new M2C_CreateUnits();
			//createUnits.Units = new Google.Protobuf.Collections.RepeatedField<UnitInfo>();
			Unit[] units = scene.GetComponent<UnitComponent>().GetAll();
			foreach (Unit u in units)
			{
				UnitInfo unitInfo = new UnitInfo();
				unitInfo.X = u.Position.x;
				unitInfo.Y = u.Position.y;
				unitInfo.Z = u.Position.z;
				unitInfo.UnitId = u.Id;
				createUnits.Units.Add(unitInfo);
			}
			MessageHelper.Broadcast(unit, createUnits);
			
			reply();
		}
	}
}