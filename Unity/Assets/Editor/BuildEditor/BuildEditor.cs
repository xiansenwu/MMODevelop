using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ET
{
	public class BundleInfo
	{
		public List<string> ParentPaths = new List<string>();
	}

	public enum PlatformType
	{
		None,
		Android,
		IOS,
		PC,
		MacOS,
	}
	
	public enum BuildType
	{
		Development,
		Release,
	}

	public class BuildEditor : EditorWindow
	{
		private readonly Dictionary<string, BundleInfo> dictionary = new Dictionary<string, BundleInfo>();

		private PlatformType platformType;
		private bool isBuildExe;
		private bool isContainAB;
		private BuildType buildType;
		private BuildOptions buildOptions = BuildOptions.AllowDebugging | BuildOptions.Development;
		private BuildAssetBundleOptions buildAssetBundleOptions = BuildAssetBundleOptions.None;

		[MenuItem("Tools/打包工具")]
		public static void ShowWindow()
		{
			GetWindow(typeof(BuildEditor));
		}
        [MenuItem("HSGameEngine/生成当前选择资源的AB文件名称 %H")]
        public static void SetAssetsBundleName()
        {
            UnityEngine.Object[] selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
            foreach (UnityEngine.Object obj in selection)
            {
                string path = AssetDatabase.GetAssetPath(obj);//选中的文件夹
                Debug.Log(path);
                var importer = AssetImporter.GetAtPath(path);
                string[] strs = path.Split('.');
                string[] dictors = strs[0].Split('/');
                string name = "";
                for (int i = 2; i < dictors.Length; i++)
                {
                    if (i < dictors.Length - 1)
                    {
                        name += dictors[i] + "/";
                    }
                    else
                    {
                        name += dictors[i];
                    }
                }
                name += ".unity3d";
                if (importer != null)
                {
                    //importer.assetBundleVariant = "bytes";
                    importer.assetBundleName = name;
                }
                else
                    Debug.Log("importer是空的");
            }


        }
        [MenuItem("HSGameEngine/清空当前选择资源的AB文件名称")]
        public static void SetAssetsBundleNameNone()
        {
            UnityEngine.Object[] selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
            foreach (UnityEngine.Object obj in selection)
            {
                string path = AssetDatabase.GetAssetPath(obj);//选中的文件夹
                Debug.Log(path);
                var importer = AssetImporter.GetAtPath(path);
               
                if (importer != null)
                {
                    //importer.assetBundleVariant = "bytes";
                    importer.assetBundleName = "";
                }
                else
                    Debug.Log("importer是空的");
            }


        }
		private void OnGUI() 
		{
			this.platformType = (PlatformType)EditorGUILayout.EnumPopup(platformType);
			this.isBuildExe = EditorGUILayout.Toggle("是否打包EXE: ", this.isBuildExe);
			this.isContainAB = EditorGUILayout.Toggle("是否同将资源打进EXE: ", this.isContainAB);
			this.buildType = (BuildType)EditorGUILayout.EnumPopup("BuildType: ", this.buildType);
			
			switch (buildType)
			{
				case BuildType.Development:
					this.buildOptions = BuildOptions.Development | BuildOptions.AutoRunPlayer | BuildOptions.ConnectWithProfiler | BuildOptions.AllowDebugging;
					break;
				case BuildType.Release:
					this.buildOptions = BuildOptions.None;
					break;
			}
			
			this.buildAssetBundleOptions = (BuildAssetBundleOptions)EditorGUILayout.EnumFlagsField("BuildAssetBundleOptions(可多选): ", this.buildAssetBundleOptions);

			if (GUILayout.Button("开始打包"))
			{
				if (this.platformType == PlatformType.None)
				{
					Log.Error("请选择打包平台!");
					return;
				}
				BuildHelper.Build(this.platformType, this.buildAssetBundleOptions, this.buildOptions, this.isBuildExe, this.isContainAB);
			}
		}
	}
}
