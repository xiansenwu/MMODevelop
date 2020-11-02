using System.IO;
using UnityEngine;

namespace ET
{
    public static class PathHelper
    {


        static string persistentDataPath;
        static string streamingAssetsPath;
        static string datafilePath;
        public static void Regiest(string streamingAssetsPath_, string persistentDataPath_,string datafilePath_)
        {
            persistentDataPath = persistentDataPath_;
            streamingAssetsPath = streamingAssetsPath_;
            datafilePath = datafilePath_;
        }

        /// <summary>
        ///应用程序外部资源路径存放路径(热更新资源路径)
        /// </summary>
        public static string AppHotfixResPath
        {
            get
            {
                //string game = Application.productName;
                string path = AppResPath;
                //if (Application.isMobilePlatform)
                {
                    path = $"{persistentDataPath}/";
                }
                return path;
            }

        }

        /// <summary>
        /// 应用程序内部资源路径存放路径
        /// </summary>
        public static string AppResPath
        {
            get
            {
                return streamingAssetsPath;
            }
        }

        /// <summary>
        /// 应用程序内部资源路径存放路径(www/webrequest专用)
        /// </summary>
        public static string AppResPath4Web
        {
            get
            {
#if UNITY_IOS || UNITY_STANDALONE_OSX
                return $"file://{streamingAssetsPath}";
#else
                return streamingAssetsPath;
#endif

            }
        }
        public static string DatafilePath
        {
            get
            {
                return datafilePath;
            }
        }
        public static string GetDatafilePath(string path)
        {
            return Path.Combine(DatafilePath, path);
        }
    }
}
