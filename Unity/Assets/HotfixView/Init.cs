using System;
using System.Threading;
using UnityEngine;
using ET;
using UnityEditor;

using Logic;
using System.IO;
using System.Reflection;
using System.Linq;

public class Init : MonoBehaviour
{
    //是否运行Bundle模式
    public bool IsRunBundle = true;
    //是否远程下载
    public bool IsRemoteDown = false;

    public static Init Instance;
    private async void Start()
    //private void Start()
    {
        //GL.Clear(false, true, Color.black);
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Setting();
        try
        {

            SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);


            string[] assemblyNames = {  "Unity.Model.dll", "Unity.ModelView.dll", "Unity.Hotfix.dll", "Unity.HotfixView.dll" };

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                string assemblyName = assembly.ManifestModule.Name;
                if (!assemblyNames.Contains(assemblyName))
                {
                    continue;
                }
                Game.EventSystem.Add(assembly);
            }
//             Game.EventSystem.Add(assemblyNames[0],typeof(Entity).Assembly);
//             Game.EventSystem.Add(assemblyNames[1],typeof(Unit).Assembly);
//             Game.EventSystem.Add(assemblyNames[2],typeof(UIForm).Assembly);
            PlayerPrefsHelper.Init();
            Game.Scene.AddComponent<TimerComponent>();
            Game.Scene.AddComponent<NetOuterComponent>();  //GateNetOuterComponent 
            Game.Scene.AddComponent<ResourcesComponent>();//初始化资源管理
            Game.Scene.AddComponent<UIComponent>();
            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatcherComponent>();//初始化消息分发组件
            Game.Scene.AddComponent<SceneChangeComponent>();
            Game.Scene.AddComponent<UnitComponent>();

            try
            {

                StartFacade.Instance.StartUp();
                await StartFacade.Instance.Run();
            }
            catch(Exception ex)
            {
                Log.Error(ex);
            }
#if UNITY_EDITOR //如果是处于编辑器模式，那么监听playmodel改变事件以模拟游戏退出
            UnityEditor.EditorApplication.playModeStateChanged += (UnityEditor.PlayModeStateChange state) =>
            {
                if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
                {
                    OnApplicationQuit();
                }
            };
#endif

        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    private void Update()
    {
        OneThreadSynchronizationContext.Instance.Update();
        Game.EventSystem.Update();
    }

    private void LateUpdate()
    {
        Game.EventSystem.LateUpdate();
    }

    private void OnApplicationQuit()
    {
        Game.Close();
    }





    private void Setting()
    {
        //setting
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        ////QualitySettings.SetQualityLevel(1);
        Application.runInBackground = true;

        //Define
        Define.IsRemoteDown = IsRemoteDown;
        Define.IsRunBundle = IsRunBundle;
#if !UNITY_EDITOR
            Define.IsRunBundle = true;
#endif
        Define.IsAsync = Define.IsRunBundle;
        //设置pc版 分辨率
#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
        Screen.SetResolution(UIComponent.designResolutionWight/2, UIComponent.designResolutionHeight/2, false);
#endif
        //path
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        string streamingAssetsPath = Application.dataPath + "/../../Release/{0}/StreamingAssets/";
        string type = "PC";
#if UNITY_ANDROID
            type = "Android";
#endif

#if UNITY_IOS
                type = "IOS";
#endif
        string fold = string.Format(streamingAssetsPath, type);
        string datapath = Application.persistentDataPath + "/"+Application.productName + "/";
        if (!Define.IsRunBundle)
        {
            datapath = Application.dataPath + "/" + "[Resources]/Config/";
        }
        else if (Define.IsRunBundle && Define.IsRemoteDown == false)
        {
            datapath = fold;
        }
        PathHelper.Regiest(fold, Application.persistentDataPath, datapath + "datafile");
#else
            string datapath = Application.persistentDataPath+ "/";
                PathHelper.Regiest(Application.streamingAssetsPath+"/", Application.persistentDataPath,datapath+"datafile"); ;
#endif
        Log.Info(string.Format("AppHotfixResPath:{0} AppResPath:{1} AppResPath4Web:{2} tempPath:{3}",
            PathHelper.AppHotfixResPath, PathHelper.AppResPath, PathHelper.AppResPath4Web, Application.temporaryCachePath));
        //FairyGUI
        //FairyGUI.GRoot.inst.SetContentScaleFactor(1080, 1920, FairyGUI.UIContentScaler.ScreenMatchMode.MatchWidth);
        
        

    }



}
