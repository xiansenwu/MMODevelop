using FairyGUI;
using UnityEngine;

namespace Logic
{
    public class ExtendGLoader : GLoader
    {
        override protected void LoadExternal()
        {
            /*
            开始外部载入，地址在url属性
            载入完成后调用OnExternalLoadSuccess
            载入失败调用OnExternalLoadFailed
            注意：如果是外部载入，在载入结束后，调用OnExternalLoadSuccess或OnExternalLoadFailed前，
            比较严谨的做法是先检查url属性是否已经和这个载入的内容不相符。
            如果不相符，表示loader已经被修改了。
            这种情况下应该放弃调用OnExternalLoadSuccess或OnExternalLoadFailed。
            */
            Texture2D tex = null;
#if UNITY_EDITOR
            Object obj = UnityEditor.AssetDatabase.LoadMainAssetAtPath("Assets/[Resources]/UIRes/" + url);
            if(obj != null) tex = obj as Texture2D;
#endif
            if (tex != null)
                onExternalLoadSuccess(new NTexture(tex));
            else
                onExternalLoadFailed();
        }
        override protected void FreeExternal(NTexture texture)
        {
            //释放外部载入的资源
        }
    }
}
