using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ET
{
	
	public class BundleDownloaderComponentAwakeSystem : AwakeSystem<BundleDownloaderComponent>
	{
		public override void Awake(BundleDownloaderComponent self)
		{
			self.bundles = new Queue<string>();
			self.downloadedBundles = new HashSet<string>();
			self.downloadingBundle = "";
		}
	}

	/// <summary>
	/// 用来对比web端的资源，比较md5，对比下载资源
	/// </summary>
	public class BundleDownloaderComponent : Entity
	{
        public static string persistentVersionFileName = "Version";
        VersionConfig persistentVersionConfig = null;
        private VersionConfig remoteVersionConfig;
		
		public Queue<string> bundles;

		public long TotalSize;

		public HashSet<string> downloadedBundles;

		public string downloadingBundle;
        public string errorInfo;
		public UnityWebRequestAsync webRequest;
		
		public override void Dispose()
		{
				if (this.IsDisposed)
				{
						return;
				}

				if (this.Parent.IsDisposed)
				{
						return;
				}

				base.Dispose();

				this.remoteVersionConfig = null;
				this.TotalSize = 0;
				this.bundles = null;
				this.downloadedBundles = null;
				this.downloadingBundle = null;
				this.webRequest?.Dispose();

				this.Parent.RemoveComponent<BundleDownloaderComponent>();
		}

	

		public int Progress
		{
			get
			{
				if (this.TotalSize == 0)
				{
					return 0;
				}

				long alreadyDownloadBytes = 0;
				foreach (string downloadedBundle in this.downloadedBundles)
				{
					long size = this.remoteVersionConfig.FileInfoDict[downloadedBundle].Size;
					alreadyDownloadBytes += size;
				}
				if (this.webRequest != null)
				{
					alreadyDownloadBytes += (long)this.webRequest.Request.downloadedBytes;
				}
				return (int)(alreadyDownloadBytes * 100f / this.TotalSize);
			}
		}
        
        public async ETTask DownloadAsync(VersionConfig _persistentVersion, VersionConfig _remoteVersion, string assetBundleServerUrl_)
		{

            if (this.bundles.Count == 0 && this.downloadingBundle == "")
			{
                Log.Debug("--------------下载 返回  ");
                return;
			}
            persistentVersionConfig = _persistentVersion;
            remoteVersionConfig = _remoteVersion;

            try
			{
				while (true)
				{
					if (this.bundles.Count == 0)
					{
                        Log.Debug("--------------下载 数量为0  跳出 ");
                        break;
					}

					this.downloadingBundle = this.bundles.Dequeue();

					while (true)
					{
						try
						{
							using (this.webRequest = this.AddComponent<UnityWebRequestAsync>())
							{
                                Log.Info("下载文件:{0}", assetBundleServerUrl_ +  this.downloadingBundle);
								await this.webRequest.DownloadAsync(assetBundleServerUrl_ + this.downloadingBundle);
								byte[] data = this.webRequest.Request.downloadHandler.data;

								string path = Path.Combine(PathHelper.AppHotfixResPath, this.downloadingBundle);
                                DirectoryInfo directory = Directory.GetParent(path);
                                while(!directory.Exists)
                                {
                                    Directory.CreateDirectory(directory.FullName);
                                    directory = directory.Parent;
                                }
								using (FileStream fs = new FileStream(path, FileMode.Create))
								{
									fs.Write(data, 0, data.Length);
								}
                                FileVersionInfo _FileVersionInfo = this.remoteVersionConfig.FileInfoDict[this.downloadingBundle];
                                persistentVersionConfig.Update(_FileVersionInfo.File, _FileVersionInfo);
                                string bytes = JsonHelper.ToJson(persistentVersionConfig);
                                PlayerPrefsHelper.Save(persistentVersionFileName, bytes);
                            }
						}
						catch (Exception e)
						{
                            errorInfo = $"download bundle error: {this.downloadingBundle}\n{e}";
                            Log.Error(errorInfo);
                            continue;
						}

						break;
					}
					this.downloadedBundles.Add(this.downloadingBundle);
					this.downloadingBundle = "";
					this.webRequest = null;
				}
			}
			catch (Exception e)
			{
                errorInfo = e.ToString();
                Log.Error(errorInfo);
			}
		}
	}
}
