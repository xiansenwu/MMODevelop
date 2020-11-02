

using Alphas.Tiled;

namespace ET
{
    public static class SceneFactory
    {
        public static async ETTask<Scene> Create(Entity parent, string name, SceneType sceneType)
        {
            long id = IdGenerater.GenerateId();
            return await Create(parent, id, parent.DomainZone(), name, sceneType);
        }
        
        public static async ETTask<Scene> Create(Entity parent, long id, int zone, string name, SceneType sceneType, StartSceneConfig startSceneConfig = null)
        {
            Scene scene = EntitySceneFactory.CreateScene(id, zone, sceneType, name);
            scene.Parent = parent;

            scene.AddComponent<MailBoxComponent, MailboxType>(MailboxType.UnOrderMessageDispatcher);
            var zoneConfig = StartZoneConfigCategory.Instance.Get(startSceneConfig.Zone);
            switch (scene.SceneType)
            {
                case SceneType.Realm:
                    scene.AddComponent<NetOuterComponent, string>(startSceneConfig.OuterAddress);
                    scene.AddComponent<DBComponent,string,string>(zoneConfig.DBConnection, zoneConfig.DBName);
                    break;
                case SceneType.Gate:
                    scene.AddComponent<NetOuterComponent, string>(startSceneConfig.OuterAddress);
                    scene.AddComponent<PlayerComponent>();
                    scene.AddComponent<GateSessionKeyComponent>();
                    scene.AddComponent<DBComponent, string, string>(zoneConfig.DBConnection, zoneConfig.DBName);
                    break;
                case SceneType.Map:
                    scene.AddComponent<UnitComponent>();
                    string tmx = ConfigHelper.GetTextTmx("WorldMap");
                    scene.AddComponent<TileMapCompnent>().Parse(tmx) ;
                    scene.AddComponent<DBComponent, string, string>(zoneConfig.DBConnection, zoneConfig.DBName);
                    break;
                case SceneType.Location:
                    scene.AddComponent<LocationComponent>();
                    //scene.AddComponent<DBComponent, string, string>(zoneConfig.DBConnection, zoneConfig.DBName);
                    break;
            }

            return scene;
        }
    }
}