using ET;
using System.Collections.Generic;
namespace ET
{
/// <summary>
/// 传送unit
/// </summary>
	[Message(InnerOpcode.M2M_TrasferUnitRequest)]
	public partial class M2M_TrasferUnitRequest: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

		public Unit Unit { get; set; }

	}

	[Message(InnerOpcode.M2M_TrasferUnitResponse)]
	public partial class M2M_TrasferUnitResponse: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long InstanceId { get; set; }

	}

	[Message(InnerOpcode.M2A_Reload)]
	public partial class M2A_Reload: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

	}

	[Message(InnerOpcode.A2M_Reload)]
	public partial class A2M_Reload: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

//创建unit
	[Message(InnerOpcode.G2M_CreateUnit)]
	public partial class G2M_CreateUnit: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

		public long PlayerId { get; set; }

		public long GateSessionId { get; set; }

	}

	[Message(InnerOpcode.M2G_CreateUnit)]
	public partial class M2G_CreateUnit: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public UnitInfo mUnit { get; set; }

	}

//获得地图信息
	[Message(InnerOpcode.G2M_MapInfo)]
	public partial class G2M_MapInfo: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

		public int Index { get; set; }

		public long GateSessionId { get; set; }

	}

	[Message(InnerOpcode.M2G_MapInfo)]
	public partial class M2G_MapInfo: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

// 所有的unit
// 所有的unit
		public List<UnitInfo> Units = new List<UnitInfo>();

	}

}
