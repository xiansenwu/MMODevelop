using System;
using System.IO;
using ET;
//using Gameproto;
using Logic;
using UnityEditor;
using UnityEngine;

namespace LogicEditor
{
    public static class LogicDataEditor
    {
#if UNITY_EDITOR
        
        [MenuItem("Tools/清理PlayerPrefs的数据")]
        public static void clearPlayerPrefsdata()
        {
            PlayerPrefsHelper.DeleteAll();
        }
        [MenuItem("Tools/导出PlayerPrefs的数据")]
        public static void outputPlayerPrefsdata()
        {
            PlayerPrefsHelper.OutputPlayerPrefsData();
        }
#endif
    }
}