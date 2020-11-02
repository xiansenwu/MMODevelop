using ET;
namespace ET
{
	[Message(LogicOpcode.C2R_Login)]
	public partial class C2R_Login : IRequest {}

	[Message(LogicOpcode.R2C_Login)]
	public partial class R2C_Login : IResponse {}

	[Message(LogicOpcode.C2G_LoginGate)]
	public partial class C2G_LoginGate : IRequest {}

	[Message(LogicOpcode.G2C_LoginGate)]
	public partial class G2C_LoginGate : IResponse {}

	public partial class Msg_PlayerInfo : IMessage {}

	[Message(LogicOpcode.C2G_PlayerInfo)]
	public partial class C2G_PlayerInfo : IRequest {}

	[Message(LogicOpcode.G2C_PlayerInfo)]
	public partial class G2C_PlayerInfo : IResponse {}

//worldMessage
//进入地图
	[Message(LogicOpcode.C2G_EnterMap)]
	public partial class C2G_EnterMap : IRequest {}

	[Message(LogicOpcode.G2C_EnterMap)]
	public partial class G2C_EnterMap : IResponse {}

// 自己的unit id
//获得地图信息
	[Message(LogicOpcode.C2G_MapInfo)]
	public partial class C2G_MapInfo : IRequest {}

	[Message(LogicOpcode.G2C_MapInfo)]
	public partial class G2C_MapInfo : IResponse {}

// 所有的unit
//创建unit
	[Message(LogicOpcode.M2C_CreateUnits)]
	public partial class M2C_CreateUnits : IActorMessage {}

	public partial class Frame_ClickMap : IActorLocationMessage {}

	[Message(LogicOpcode.M2C_PathfindingResult)]
	public partial class M2C_PathfindingResult : IActorMessage {}

}
namespace ET
{
	public static partial class LogicOpcode
	{
		 public const ushort C2R_Login = 10001;
		 public const ushort R2C_Login = 10002;
		 public const ushort C2G_LoginGate = 10003;
		 public const ushort G2C_LoginGate = 10004;
		 public const ushort C2G_PlayerInfo = 10005;
		 public const ushort G2C_PlayerInfo = 10006;
		 public const ushort C2G_EnterMap = 10101;
		 public const ushort G2C_EnterMap = 10102;
		 public const ushort C2G_MapInfo = 10103;
		 public const ushort G2C_MapInfo = 10104;
		 public const ushort M2C_CreateUnits = 10105;
		 public const ushort M2C_PathfindingResult = 10106;
	}
}
