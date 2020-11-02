using ET;

namespace Logic
{
	public class GateNetOuterComponentAwakeSystem : AwakeSystem<GateNetOuterComponent>
	{
		public override void Awake(GateNetOuterComponent self)
		{
			self.Awake(self.Protocol);
			self.MessagePacker = new ProtobufPacker();
			self.MessageDispatcher = new OuterMessageDispatcher();
		}
	}
	public class GateNetOuterComponentAwake1System : AwakeSystem<GateNetOuterComponent, string>
	{
		public override void Awake(GateNetOuterComponent self, string address)
		{
			self.Awake(self.Protocol, address);
			self.MessagePacker = new ProtobufPacker();
			self.MessageDispatcher = new OuterMessageDispatcher();
		}
	}
	public class GateNetOuterComponentLoadSystem : LoadSystem<GateNetOuterComponent>
	{
		public override void Load(GateNetOuterComponent self)
		{
			self.MessagePacker = new ProtobufPacker();
			self.MessageDispatcher = new OuterMessageDispatcher();
		}
	}
	public class GateNetOuterComponentUpdateSystem : UpdateSystem<GateNetOuterComponent>
	{
		public override void Update(GateNetOuterComponent self)
		{
			self.Update();
		}
	}


}