
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LoadLogicComponentAwakeSystem : AwakeSystem<LoadLogicComponent>
    {
        public override void Awake(LoadLogicComponent t)
        {
            t.Awake();
        }
    }
    public class LoadLogicComponent : Entity
    {
        public void Awake()
        {

        }
        public async ETTask Run()
        {
            await Game.EventSystem.Publish(new EventIdType.LoadingBegin());//加载
            await BundleHelper.LoadBundle();
            //await Game.Scene.GetComponent<TimerComponent>().WaitAsync(10000);
            //加载
            //BundleHelper.LoadBundleStreamingAssets();
            //新Ui
            Log.Debug("加载UI框架");
            UIPackageHelp.AddPackage("Base");
            UIPackageHelp.AddPackage("Common");
            //Game.Scene.AddComponent<UIManagerComponent>();//初始化UI管理
            //声音
            // Game.Scene.AddComponent<SoundComponent>();//初始化声音管理
            LoadData();//先放这
            //                 Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("config.unity3d"); // 加载配置
            //                 Game.Scene.AddComponent<ConfigComponent>();//配置序列化到内存
            //                 Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("config.unity3d");//卸载配置
            await Game.EventSystem.Publish(new EventIdType.LoadingFinish());//关闭加载界面
        }
        public void LoadData()
        {
            Log.Debug("LoadData  ");
            ResourcesComponent.Instance.LoadBundle("config.unity3d");
            Game.Scene.AddComponent<ConfigComponent>();
            ResourcesComponent.Instance.UnloadBundle("config.unity3d");
            Dictionary<Type, ACategory> AllConfig = ConfigComponent.Instance.AllConfig;
            Log.Debug("LoadData  "+ AllConfig.Count);
        }
    }
}
