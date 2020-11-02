using ET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UpdateLogicComponentAwakeSystem : AwakeSystem<UpdateLogicComponent>
    {
        public override void Awake(UpdateLogicComponent t)
        {
            t.Awake();
        }
    }
    public class UpdateLogicComponent : Entity
    {
        public void Awake()
        {

        }
        public async ETTask Run()
        {
            BundleDownloaderComponent _BundleDownloaderComponent = Game.Scene.GetComponent<BundleDownloaderComponent>();
            string assetBundleServerUrl_ = GlobalConfigComponent.Instance.GlobalProto.GetUrl() + "StreamingAssets/";
            
            VersionConfig persistentVersionConfig = GlobalConfigComponent.Instance.persistentVersionConfig;
            if (Define.IsRemoteDown)
            {
                await Game.EventSystem.Publish(new EventIdType.UpdateBegin());//更新
                // 获取远程的Version.txt
                string versionUrl = "";
                try
                {
                    using (UnityWebRequestAsync webRequestAsync = this.AddComponent<UnityWebRequestAsync>())
                    {
                        versionUrl = assetBundleServerUrl_  + "Version.txt";
                        Log.Debug(versionUrl);
                        await webRequestAsync.DownloadAsync(versionUrl);
                        GlobalConfigComponent.Instance.remoteVersionConfig = JsonHelper.FromJson<VersionConfig>(webRequestAsync.Request.downloadHandler.text);
                        //Log.Debug(JsonHelper.ToJson(this.VersionConfig));
                    }

                }
                catch (Exception e)
                {
                    throw new Exception($"url: {versionUrl}", e);
                }
                VersionConfig remoteVersionConfig = GlobalConfigComponent.Instance.remoteVersionConfig;
                // 删掉远程不存在的文件
                DirectoryInfo directoryInfo = new DirectoryInfo(PathHelper.AppHotfixResPath);
                if (directoryInfo.Exists)
                {
                    //暂时不删除 
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        if (remoteVersionConfig.FileInfoDict.ContainsKey(fileInfo.Name))
                        {
                            continue;
                        }

                        if (fileInfo.Name == "Version.txt")
                        {
                            continue;
                        }

                        fileInfo.Delete();
                    }
                }
                else
                {
                    directoryInfo.Create();
                }

                // 对比MD5
                foreach (FileVersionInfo fileVersionInfo in remoteVersionConfig.FileInfoDict.Values)
                {
                    // 对比md5
                    string persistentFileMD5 = BundleHelper.GetBundleMD5(persistentVersionConfig, fileVersionInfo.File);
                    if (fileVersionInfo.MD5 == persistentFileMD5)
                    {
                        continue;
                    }
                    _BundleDownloaderComponent.bundles.Enqueue(fileVersionInfo.File);
                    _BundleDownloaderComponent.TotalSize += fileVersionInfo.Size;
                }
                // 更新 下载ab包
                //await BundleHelper.DownloadBundle(assetBundleServerUrl_);//下载AB包(热更资源)
                await _BundleDownloaderComponent.DownloadAsync(persistentVersionConfig, remoteVersionConfig, assetBundleServerUrl_);
                persistentVersionConfig.Version = remoteVersionConfig.Version;
                string bytes = JsonHelper.ToJson(persistentVersionConfig);
                PlayerPrefsHelper.Save(BundleDownloaderComponent.persistentVersionFileName, bytes);
                //更新 end
                await Game.EventSystem.Publish(new EventIdType.UpdateFinish());
            }
        }
    }
}
