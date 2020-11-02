

using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    
    public class PlayerPrefsKeys
    {
        public List<string> Keys = new List<string>();
    }


    public class PlayerPrefsHelper
    {
        static string PlayerPrefsKeysStr = "";
        static PlayerPrefsKeys playerPrefsKeys = null;
        public static void Save(object data)
        {
            string Datastr = JsonHelper.ToJson(data);
            string key = data.GetType().ToString();
            Save(key, Datastr);
        }
        public static void Save(string key, object data)
        {
            string Datastr = JsonHelper.ToJson(data);
            Save(key, Datastr);
        }

        public static void Save(string key, string dataStr)
        {
            PlayerPrefs.SetString(key, dataStr);
            AddKey(key);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            OutputPlayerPrefsData();
#endif
        }
        public static T Get<T>() where T : class
        {

            Type type = typeof(T);
            string key = type.ToString();
            return Get(key, type) as T;
        }
        public static T Get<T>(string key) where T : class
        {

            Type type = typeof(T);
            T _T = Get(key, type) as T;
            return _T;
        }
        public static object Get(string key,Type type) 
        {
            string Datastr = PlayerPrefs.GetString(key);
            if (string.IsNullOrEmpty(Datastr))
            {
                return Activator.CreateInstance(type);
            }
            if (type == typeof(int))
                return int.Parse(Datastr);
            else if (type == typeof(uint))
                return uint.Parse(Datastr);
            else if (type == typeof(double))
                return double.Parse(Datastr);
            else if (type == typeof(float))
                return float.Parse(Datastr);
            else if (type == typeof(bool))
                return bool.Parse(Datastr);
            else if (type == typeof(string))
                return Datastr;
            object _T = JsonHelper.FromJson(type,Datastr);
            return _T;
        }
        public static void Init()
        {
            PlayerPrefsKeysStr = typeof(PlayerPrefsKeys).ToString();
            LoadAllKey();
        }
        private static void LoadAllKey()
        {
            string PlayerPrefsKeysstr = PlayerPrefs.GetString(PlayerPrefsKeysStr);
            if(string.IsNullOrEmpty(PlayerPrefsKeysstr)) 
            {
                playerPrefsKeys = new PlayerPrefsKeys();
            }
            else
            {
                playerPrefsKeys = Get<PlayerPrefsKeys>();
            }
        }
        private static void AddKey(string key)
        {
            if(!playerPrefsKeys.Keys.Contains(key))
            {
                playerPrefsKeys.Keys.Add(key);
            }
            string Datastr = JsonHelper.ToJson(playerPrefsKeys);
            PlayerPrefs.SetString(PlayerPrefsKeysStr, Datastr);
        }
        public static void OutputPlayerPrefsData()
        {
            if(playerPrefsKeys == null)
            {
                LoadAllKey();
            }
            string _PlayerPrefsKeysStr = typeof(PlayerPrefsKeys).ToString();
            string _Datastr = UnityEngine.PlayerPrefs.GetString(_PlayerPrefsKeysStr);
            string playerPrefsdData = Application.dataPath + "/../../PlayerPrefsdData/";
            if (!Directory.Exists(playerPrefsdData))
            {
                Directory.CreateDirectory(playerPrefsdData);
            }
#if !ILRuntime
            string keyDataPath0 = playerPrefsdData + _PlayerPrefsKeysStr + ".txt";
            string _keyDatastr0 = UnityEngine.PlayerPrefs.GetString(_PlayerPrefsKeysStr);

            if (File.Exists(keyDataPath0))
            {
                File.Delete(keyDataPath0);
            }
            File.WriteAllText(keyDataPath0, _keyDatastr0);

            foreach (string key in playerPrefsKeys.Keys)
            {
                string keyDataPath = playerPrefsdData + key+".txt";
                string _keyDatastr = UnityEngine.PlayerPrefs.GetString(key);
                if (string.IsNullOrEmpty(_keyDatastr)) continue;
                if (File.Exists(keyDataPath))
                {
                    File.Delete(keyDataPath);
                }
                File.WriteAllText(keyDataPath, _keyDatastr);
            }
#endif
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static float GetFloat(string key, float defaultValue)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public static float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public static int GetInt(string key, int defaultValue)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
        
        public static int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public static string GetString(string key, string defaultValue)
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public static string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }

        public static void SetFloat(string key, float value)
        {
            AddKey(key);
            PlayerPrefs.SetFloat(key, value);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            OutputPlayerPrefsData();
#endif
        }

        public static void SetInt(string key, int value)
        {
            AddKey(key);
            PlayerPrefs.SetInt(key, value);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            OutputPlayerPrefsData();
#endif
        }

        public static void SetString(string key, string value)
        {
            AddKey(key);
            PlayerPrefs.SetString(key, value);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            OutputPlayerPrefsData();
#endif
        }
    }
}
