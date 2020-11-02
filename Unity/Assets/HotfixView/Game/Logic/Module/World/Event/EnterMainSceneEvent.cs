using System;
using System.Net;
using Alphas;
using ET;
using FairyGUI;

using UnityEngine;


namespace Logic
{

    public class EnterMainSceneEvent : AEvent<EventIdType.EnterMainSceneEvent>
    {
        

        public override async ETTask Run(EventIdType.EnterMainSceneEvent a)
        {
            try
            {
                //test
                //                 LobbyClient.Instance.AccountInfo.AccountId = "1";
                //                 RoleInfo roleInfo = new RoleInfo();
                //                 roleInfo.Guid = 1;
                //                 LobbyClient.Instance.AccountInfo.Roles.Clear();
                //                 LobbyClient.Instance.AccountInfo.Roles.Add(roleInfo);
                //                 LobbyClient.Instance.CurrentRole = roleInfo;

                // SceneManager_sceneLoaded(1000);
                //Game.Scene.GetComponent<SLDNetComponent>().Connect();
                
                //Game.Scene.AddComponent<CharacterManager>();
                await Game.Scene.GetComponent<SceneChangeComponent>().ChangeSceneAsync("Map");
                //ResourceSystem.GetSharedResource("scenes/mapdata_1000/scene1000");

                // LogicSystem.QueueLogicAction<uint>(SceneManager.Instance.ChangeSceneAsync, 1000);
                // SceneManager.Instance.ChangeSceneAsync(1000, SceneManager_sceneLoaded);
                //ET.Game.EventSystem.Run(ET.EventIdType.LoadingFinish);//关闭加载界面
    


            }
            catch(Exception ex)
            {
                Log.Error("---------------ex "+ ex.ToString());
            }

        }//run_end

        private void SceneManager_sceneLoaded(uint id)
        {
            //UIComponent.Instance.CloseAllWindows();
        }




    }//class_end
}
