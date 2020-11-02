using System.IO;

namespace ET
{
    public class SessionComponentAwakeSystem : AwakeSystem<SessionComponent, Session>
    {
        public override void Awake(SessionComponent self, Session session)
        {
            self.Session = session;
            SessionCallbackComponent sessionComponent = session.AddComponent<SessionCallbackComponent>();
            sessionComponent.MessageCallback = (s, opcode, memory) => { self.MessageCallback(s, opcode, memory); };
            //sessionComponent.DisposeCallback = s => { self.Dispose(); };
        }
    }
    public class SessionComponentDestroySystem: DestroySystem<SessionComponent>
	{
		public override void Destroy(SessionComponent self)
		{
			self.Session.Dispose();
		}
	}
    public static class SessionComponentSystem
    {
        public static void MessageCallback(this SessionComponent self, Session s, ushort opcode, MemoryStream memory)
        {
            int a = 1;
            a++;
        }
    }
    
}
