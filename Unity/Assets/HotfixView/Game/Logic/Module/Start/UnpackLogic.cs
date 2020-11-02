using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Logic
{

    public class UnpackLogicComponentAwakeSystem : AwakeSystem<UnpackLogicComponent>
    {
        public override void Awake(UnpackLogicComponent t)
        {
            t.Awake();
        }
    }
    public class UnpackLogicComponent : Entity
    {
        public void Awake()
        {

        }
        public async ETTask Run()
        {
            
            
            if (Define.IsRemoteDown || Application.isMobilePlatform)
            {
                await Game.EventSystem.Publish(new EventIdType.UnpackBegin());//解压
                await GlobalConfigComponent.Instance.LoadStreamingVersionConfig();//StreamingVersionConfig
                GlobalConfigComponent.Instance.LoadPersistentVersionConfig();//PersistentVersionConfig

                VersionConfig streamingVersionConfig = GlobalConfigComponent.Instance.streamingVersionConfig;
                VersionConfig persistentVersionConfig = GlobalConfigComponent.Instance.persistentVersionConfig;
                BundleDownloaderComponent _BundleDownloaderComponent = Game.Scene.GetComponent<BundleDownloaderComponent>();
                if (streamingVersionConfig.Version > persistentVersionConfig.Version)
                {
                    // 对比MD5
                    foreach (FileVersionInfo fileVersionInfo in streamingVersionConfig.FileInfoDict.Values)
                    {
                        // 对比md5
                        string persistentFileMD5 = BundleHelper.GetBundleMD5(persistentVersionConfig, fileVersionInfo.File);

                        if (fileVersionInfo.MD5 != persistentFileMD5)
                        {
                            if (fileVersionInfo.resourceType == ResourceType.Text)
                            {
                                _BundleDownloaderComponent.bundles.Enqueue(fileVersionInfo.File);
                                _BundleDownloaderComponent.TotalSize += fileVersionInfo.Size;
                            }
                            continue; ;
                        }
                    }
                }
                string assetBundleServerUrl_ = PathHelper.AppResPath4Web;
                await _BundleDownloaderComponent.DownloadAsync(persistentVersionConfig, streamingVersionConfig, assetBundleServerUrl_);

                persistentVersionConfig.Version = streamingVersionConfig.Version;
                string bytes = JsonHelper.ToJson(persistentVersionConfig);
                PlayerPrefsHelper.Save(BundleDownloaderComponent.persistentVersionFileName, bytes);
                await Game.EventSystem.Publish(new EventIdType.UnpackFinish());
            }
        }
    }
}
