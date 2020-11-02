/***
 * 开始下载。
 */

using ET;
using FairyGUI;
using System;

namespace Logic
{
    public class LodingBeginEvent:AEvent<EventIdType.LoadingBegin>
    {

        public override async ETTask Run(EventIdType.LoadingBegin a)
        {
            //throw new NotImplementedException();
        }
    }


    public class LodingFinishEvent : AEvent<EventIdType.LoadingFinish>
    {
        int curLevel = 1;
        public override async ETTask Run(EventIdType.LoadingFinish a)
        {
           // Platform.PlatformWindow.Instance.Hide();
            //Platform.PlatformWindow.Instance.Destroy();
            //UIPackage.RemovePackage("UI/Platform");

            //LogicSystem.QueueLogicAction<uint>(SceneManager.Instance.ChangeSceneAsync, 1000);
           // SceneManager.Instance.ChangeSceneAsync((uint)1000, SceneManager_sceneLoaded);
            //Data_LevelConfig _Data_LevelConfig = LevelConfigProvider.Instance.GetLevelConfigById(curLevel);
            /*
            if (_Data_LevelConfig != null)
            {

                SceneManager.Instance.ChangeSceneAsync((uint)1000, SceneManager_sceneLoaded);
                // SceneManager.Instance.ChangeScene(_Data_LevelConfig.m_SceneID, SceneManager_sceneLoaded);
            }
            else
            {
                Log.Error("Data_LevelConfig no find :{0}", curLevel);
            }
            */
        }

        void SceneManager_sceneLoaded()
        {
            try
            {

                //LevelManager.Instance.EnterLevel(curLevel);
                //SceneManager.Instance.ChangeScene(1000);
                //LoadingUI.Instatnce?.CloseLoadingUI();
            }
            catch (Exception ex)
            {
                //Log.Error(ex.ToString());
            }

        }
    }
}