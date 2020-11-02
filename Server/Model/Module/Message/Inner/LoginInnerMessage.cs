using ET;
using System.Collections.Generic;
namespace ET
{
	[Message(InnerOpcode.R2G_GetLoginKey)]
	public partial class R2G_GetLoginKey: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

		public string Account { get; set; }

	}

	[Message(InnerOpcode.G2R_GetLoginKey)]
	public partial class G2R_GetLoginKey: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long Key { get; set; }

		public long GateId { get; set; }

	}

	[Message(InnerOpcode.R2G_Register)]
	public partial class R2G_Register: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

		public string Account { get; set; }

		public string Password { get; set; }

	}

	[Message(InnerOpcode.G2R_Register)]
	public partial class G2R_Register: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long Key { get; set; }

		public long GateId { get; set; }

	}

}
