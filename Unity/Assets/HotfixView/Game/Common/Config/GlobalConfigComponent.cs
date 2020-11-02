using ET;
using System;
using System.IO;
using UnityEngine;

namespace Logic
{
	public class GlobalConfigComponentAwakeSystem : AwakeSystem<GlobalConfigComponent>
	{
		public override void Awake(GlobalConfigComponent t)
		{
			t.Awake();
		}
	}
   
    public class GlobalConfigComponent : Entity
	{
        
        public static GlobalConfigComponent Instance;
		public GlobalProto GlobalProto;
        public VersionConfig streamingVersionConfig;
        public VersionConfig persistentVersionConfig;
        public VersionConfig remoteVersionConfig;
        public RemoteVersion remoteCodeVersionInfo;

        public PackageConfig packageConfig;//包的相关信息
        public DeubgConfig deubgConfig;//调试的相关配置
        public StatisticsConfig statisticsConfig;//统计相关信息

        public void Awake()
		{
			Instance = this;
            GameObject config = (GameObject)ResourcesHelper.Load("KV");
            string configStr = config.Get<TextAsset>("GlobalProto").text;
			this.GlobalProto = JsonHelper.FromJson<GlobalProto>(configStr);
            //this.packageConfig = JsonHelper.FromJson<PackageConfig>(configStr);

        }
        public async ETTask LoadStreamingVersionConfig()
        {
            try
            {
                string versionPath = Path.Combine(PathHelper.AppResPath4Web, "Version.txt");
                using (UnityWebRequestAsync request = this.AddComponent<UnityWebRequestAsync>())
                {
                    Log.Info("本地version地址：{0}", versionPath);
                    await request.DownloadAsync(versionPath);
                    streamingVersionConfig = JsonHelper.FromJson<VersionConfig>(request.Request.downloadHandler.text);
                }
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
        public void LoadPersistentVersionConfig()
        {
            string VersionFileName = BundleDownloaderComponent.persistentVersionFileName;
            try
            {
                // 获取本地目录的Version.txt
                string persistentVersionStr = PlayerPrefsHelper.GetString(VersionFileName);
                if (!string.IsNullOrEmpty(persistentVersionStr))
                {
                    persistentVersionConfig = JsonHelper.FromJson<VersionConfig>(persistentVersionStr);
                }
                else
                {
                    persistentVersionConfig = new VersionConfig();
                    persistentVersionConfig.Version = 0;
                    persistentVersionConfig.TotalSize = 0;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}