﻿using System;
using System.IO;

namespace ET
{
	public static class ConfigHelper
	{
		public static string GetText(string key)
		{
			string path = $"../Config/{key}.txt";
			try
			{
				string configStr = File.ReadAllText(path);
				return configStr;
			}
			catch (Exception e)
			{
				throw new Exception($"load config file fail, path: {path} {e}");
			}
		}
		public static string GetTextTmx(string key)
		{
			string path = $"../Config/{key}.tmx";
			try
			{
				string configStr = File.ReadAllText(path);
				return configStr;
			}
			catch (Exception e)
			{
				throw new Exception($"load config Tmx file fail, path: {path} {e}");
			}
		}
		public static T ToObject<T>(string str)
		{
			return MongoHelper.FromJson<T>(str);
		}
	}
}
