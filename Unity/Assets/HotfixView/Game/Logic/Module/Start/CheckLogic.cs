using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
 
namespace Logic
{
    public class CheckLogicComponentAwakeSystem : AwakeSystem<CheckLogicComponent>
    {
        public override void Awake(CheckLogicComponent t)
        {
            t.Awake();
        }
    }
    public class CheckLogicComponent : Entity
    {
        public void Awake()
        {

        }
        public async ETTask Run()
        {
            await Game.EventSystem.Publish(new EventIdType.CheckBegin());//检查大版本
            if (Define.IsRemoteDown)
            {
                
                // 获取远程的Version.txt
                string versionUrl = "";
                string assetBundleServerUrl_ = GlobalConfigComponent.Instance.GlobalProto.GetUrl();
                try
                {
                    using (UnityWebRequestAsync webRequestAsync = this.AddComponent<UnityWebRequestAsync>())
                    {
                        versionUrl = assetBundleServerUrl_  + "RemoteVersion.txt";
                        Log.Debug(versionUrl);
                        await webRequestAsync.DownloadAsync(versionUrl);
                        GlobalConfigComponent.Instance.remoteCodeVersionInfo = JsonHelper.FromJson<RemoteVersion>(webRequestAsync.Request.downloadHandler.text);
                        //Log.Debug(JsonHelper.ToJson(this.VersionConfig));
                    }

                }
                catch (Exception e)
                {
                    //AlertManager.Instance.showAlert("版本检测出错", "获取远程文件出错RemoteVersion.txt url:" + versionUrl+ e.ToString(), () => { });
                    throw new Exception($"url: {versionUrl}", e);
                }
                RemoteVersion _RemoteVersion = GlobalConfigComponent.Instance.remoteCodeVersionInfo;
                GlobalProto _GlobalProto = GlobalConfigComponent.Instance.GlobalProto;
                string linkurl = _RemoteVersion.androidLinkurl;
#if UNITY_IOS
                linkurl = _RemoteVersion.iosLinkurl;
#endif
                if (_RemoteVersion.minCodeVersion > _GlobalProto.CodeVer)
                {
                    //this.tcs = new ETTaskCompletionSource();
                    //需要大版本更新
//                     await AlertManager.Instance.asnycShowAlert("版本更新", _RemoteVersion.minCodeVersionMSG, () => {
//                         Application.OpenURL(linkurl);
//                         Application.Quit();
//                     });
                    return;
                }
                else if (_RemoteVersion.maxCodeVersion > _GlobalProto.CodeVer)
                {
                    if (_RemoteVersion.maxCodeIsTip)
                    {
                        //提示更新

                        //需要大版本更新
//                         await AlertManager.Instance.asnycShowAlert("版本更新", _RemoteVersion.maxCodeVersionMSG, () =>
//                         {
//                             Application.OpenURL(linkurl);
//                             Application.Quit();
//                         }, () =>
//                         {
//                             
//                         });
                    }
                }
                
            }
            await Game.EventSystem.Publish( new EventIdType.CheckFinish());
        }
    }
}
