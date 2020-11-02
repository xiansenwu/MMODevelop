using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ET
{
	public static class ResourcesHelper
	{
		public static UnityEngine.Object Load(string path)
		{
			return Resources.Load(path);
		}
        public static UnityEngine.Object GetAssetByNameWithoutExtention(string assetNameWithoutExtention)
        {
            if (string.IsNullOrEmpty(assetNameWithoutExtention))
            {
                return null;
            }
            string[] assetnamses = assetNameWithoutExtention.Split('/');
            string assetsname = assetnamses[assetnamses.Length - 1];

            ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
            if (!resourcesComponent.HasAsset(assetNameWithoutExtention + ".unity3d", assetsname))
            {
                resourcesComponent.LoadBundle(assetNameWithoutExtention + ".unity3d");
            }
            UnityEngine.Object @object = resourcesComponent.GetAsset(assetNameWithoutExtention + ".unity3d", assetsname);
            if (@object == null)
            {
                @object = Resources.Load(assetNameWithoutExtention);
            }
            return @object;
        }
        public static async ETTask LoadBundleAsyncWithoutExtention(string assetNameWithoutExtention)
        {
            if (string.IsNullOrEmpty(assetNameWithoutExtention))
            {
                return;
            }
            string[] assetnamses = assetNameWithoutExtention.Split('/');
            string assetsname = assetnamses[assetnamses.Length - 1];

            ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
            if (!resourcesComponent.HasAsset(assetNameWithoutExtention + ".unity3d", assetsname))
            {
                await resourcesComponent.LoadBundleAsync(assetNameWithoutExtention + ".unity3d");
            }
        }
    }
}
