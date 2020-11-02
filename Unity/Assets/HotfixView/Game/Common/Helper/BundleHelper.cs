using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public static class BundleHelper
    {
        //public static string s_ResSheetFile = "list.txt";
        public static string s_ResSheetPattern = ".txt;.map;.dsl";
        public static string[] s_ConfigSplit = new string[] { ";", "|", };
        public static string s_ResSheetCachePath = "datafile/";

       
        public static async ETTask LoadBundle()
        {
            LoadBundleStreamingAssets();
            
        }
        public static void LoadBundleStreamingAssets()
        {
            if (Define.IsRunBundle)
            {
                Game.Scene.GetComponent<ResourcesComponent>().LoadOneBundle("StreamingAssets");
                ResourcesComponent.AssetBundleManifestObject = (AssetBundleManifest)Game.Scene.GetComponent<ResourcesComponent>().GetAsset("StreamingAssets", "AssetBundleManifest");
            }
        }
        public static string GetBundleMD5(VersionConfig _VersionConfig, string bundleName)
        {
            if (_VersionConfig != null && _VersionConfig.FileInfoDict.ContainsKey(bundleName))
            {
                return _VersionConfig.FileInfoDict[bundleName].MD5;
            }

            return "";
        }
        public static bool CheckFilePatternEndWith(string filePath, string[] pattern)
        {
            if (pattern == null || pattern.Length == 0)
            {
                return false;
            }
            foreach (string tPattern in pattern)
            {
                if (filePath.EndsWith(tPattern))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
