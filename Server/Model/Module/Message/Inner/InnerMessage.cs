using ET;
using System.Collections.Generic;
namespace ET
{
	[Message(InnerOpcode.G2G_LockRequest)]
	public partial class G2G_LockRequest: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

		public long Id { get; set; }

		public string Address { get; set; }

	}

	[Message(InnerOpcode.G2G_LockResponse)]
	public partial class G2G_LockResponse: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.G2G_LockReleaseRequest)]
	public partial class G2G_LockReleaseRequest: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

		public long Id { get; set; }

		public string Address { get; set; }

	}

	[Message(InnerOpcode.G2G_LockReleaseResponse)]
	public partial class G2G_LockReleaseResponse: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectAddRequest)]
	public partial class ObjectAddRequest: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

		public long Key { get; set; }

		public long InstanceId { get; set; }

	}

	[Message(InnerOpcode.ObjectAddResponse)]
	public partial class ObjectAddResponse: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectLockRequest)]
	public partial class ObjectLockRequest: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

		public long Key { get; set; }

		public long InstanceId { get; set; }

		public int Time { get; set; }

	}

	[Message(InnerOpcode.ObjectLockResponse)]
	public partial class ObjectLockResponse: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectUnLockRequest)]
	public partial class ObjectUnLockRequest: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

		public long Key { get; set; }

		public long OldInstanceId { get; set; }

		public long InstanceId { get; set; }

	}

	[Message(InnerOpcode.ObjectUnLockResponse)]
	public partial class ObjectUnLockResponse: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectRemoveRequest)]
	public partial class ObjectRemoveRequest: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

		public long Key { get; set; }

	}

	[Message(InnerOpcode.ObjectRemoveResponse)]
	public partial class ObjectRemoveResponse: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectGetRequest)]
	public partial class ObjectGetRequest: IActorRequest
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

		public long Key { get; set; }

	}

	[Message(InnerOpcode.ObjectGetResponse)]
	public partial class ObjectGetResponse: IActorResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }

		public long InstanceId { get; set; }

	}

	[Message(InnerOpcode.G2M_SessionDisconnect)]
	public partial class G2M_SessionDisconnect: IActorLocationMessage
	{
		public int RpcId { get; set; }

		public long ActorId { get; set; }

	}

}
