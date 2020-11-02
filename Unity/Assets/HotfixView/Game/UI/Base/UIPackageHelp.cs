using ET;
using FairyGUI;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public static class UIPackageHelp
    {
        public class UIPackageResInfo
        {
            public List<string> bundles = new List<string>();
            public uint count = 0;
            public void AddBundle(string bundleName)
            {
                if(!bundles.Contains(bundleName))
                    bundles.Add(bundleName);
            }
        }
        //记录包是否Add的字典
        private static Dictionary<string, UIPackageResInfo> _packageAddDict = new Dictionary<string, UIPackageResInfo>();
        /// <summary>
        /// 添加UIPackage
        /// 这里添加排重处理
        /// </summary>
        /// <param name="packName"></param>
        public static void AddPackage(string packageName)
        {
            UIPackageResInfo resInfo = null;
            if (_packageAddDict.ContainsKey(packageName) == false)
            {
                resInfo = new UIPackageResInfo();
                resInfo.count++;
                _packageAddDict.Add(packageName, resInfo);
            }
            else
            {
                //增加引用计数
                resInfo = _packageAddDict[packageName];
                resInfo.count++;
                return;
            }
            
            UIPackage.AddPackage(packageName, (string name, string extension, System.Type type, out DestroyMethod destroyMethod) =>
            {
                string assetNameWithoutExtention = name.ToLower();
                //ET.Game.Scene.GetComponent<ResourcesComponent>().LoadOneBundle(bundleName);
                //AssetBundle bundle = ET.Game.Scene.GetComponent<ResourcesComponent>().bundles[bundleName].AssetBundle;
                //赋值给FairyGUi。
                //UIPackage.AddPackage(bundle);
                string bundleName = "uires/uipackage/" + assetNameWithoutExtention + ".unity3d";
                Game.Scene.GetComponent<ResourcesComponent>().LoadBundle(bundleName);
                UIPackageResInfo packres = _packageAddDict[packageName];
                packres.AddBundle(bundleName);
                string[] assetnamses = assetNameWithoutExtention.Split('/');
                string assetsname = assetnamses[assetnamses.Length - 1];
                UnityEngine.Object @object = ET.Game.Scene.GetComponent<ResourcesComponent>().GetAsset(bundleName, assetsname);

                destroyMethod = DestroyMethod.None;
                return @object;
            });
        }
        public static void AddPackageFromResources(string packageName)
        {
            UIPackage.AddPackage("UI/" + packageName);
        }
        /// <summary>
        /// 检查UI包是否已经包进来
        /// </summary>
        /// <param name="packageName">UI包名</param>
        public static void RemovePackage(string packageName)
        {
            //UIPackage.unloadBundleByFGUI = false;
            UIPackageResInfo uIPackageResInfo = _packageAddDict[packageName];
            if (uIPackageResInfo == null)
                return;
            uIPackageResInfo.count--;
            if (uIPackageResInfo.count == 0)
            {
                _packageAddDict.Remove(packageName);
                UIPackage.RemovePackage(packageName);
                foreach (string bundleName in uIPackageResInfo.bundles)
                {
                    ResourcesComponent.Instance?.UnloadBundle(bundleName);
                }
            }
            
            
        }

        public static GObject CreateObject(string packageName, string resName)
        {
            AddPackage(packageName);
            return UIPackage.CreateObject(packageName, resName);
        }
        public static GObject CreateObject(string packageName, string resName,System.Type type)
        {
            AddPackage(packageName);
            return UIPackage.CreateObject(packageName, resName, type);
        }
        public static void ClearUnusePackage()
        {

        }
    }
}
