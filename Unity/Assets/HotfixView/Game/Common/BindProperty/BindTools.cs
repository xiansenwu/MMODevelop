using ET;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Logic
{
    public class BindTools
    {
        private enum EBindType
        {
            Unknow,
            Field,
            Property,
            Action,
        }
        private class BindInfo
        {
            public BindInfo(string key, object site, string prop, EBindType bindType)
            {
                Key = key;
                Site = new WeakReference<object>(site);
                Prop = prop;
                BindType = bindType;
            }
            public BindInfo(string key, Action<object> action)
            {
                Key = key;
                Action = new WeakReference<Action<object>>(action);
                BindType = EBindType.Action;
            }
            public string Key { private set; get; }
            public WeakReference<object> Site { private set; get; }
            public string Prop { private set; get; }
            public EBindType BindType { private set; get; }
            public WeakReference<Action<object>> Action { private set; get; }
        }
        static Dictionary<string, List<BindInfo>> bindDic = new Dictionary<string, List<BindInfo>>();
        static public void BindProperty(object site, string prop, object host, string chain, bool useForInit = true)
        {
            EBindType bindType = CheckSite(site, prop);
            if (bindType == EBindType.Unknow)
            {
                throw new Exception(string.Format("绑定出错！需要具有{0}.{1}属性或者变量！", site?.GetType().Name, prop));
            }
            if (!CheckHost(host, chain))
            {
                throw new Exception(string.Format("绑定出错！{0}.{1}需要是公共可设置并且具有BindEnableAttribute特性！", host?.GetType().Name, chain));
            }
            string key = host.GetHashCode() + "_" + chain;
            BindInfo bindInfo = new BindInfo(key, site, prop, bindType);
            List<BindInfo> bindList;
            if(bindDic.TryGetValue(key, out bindList))
            {
                foreach(BindInfo info in bindList)
                {
                    if(info.Site == site && info.Prop == prop)
                    {
                        throw new Exception("绑定出错！重复绑定！");
                    }
                }
                bindList.Add(bindInfo);
            }
            else
            {
                bindList = new List<BindInfo>();
                bindList.Add(bindInfo);
                bindDic.Add(key, bindList);
            }
            if(useForInit)
            {
                Type hostType = host.GetType();
                PropertyInfo hostPropertyInfo = hostType.GetProperty(chain, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                SetValue(bindInfo, hostPropertyInfo.GetValue(host));
            }
        }
        static public void BindSetter(Action<object> setter, object host, string chain, bool useForInit = true)
        {
            if (!CheckHost(host, chain))
            {
                throw new Exception(string.Format("绑定出错！{0}.{1}需要是公共可设置并且具有BindEnableAttribute特性！", host.GetType().Name, chain));
            }
            string key = host.GetHashCode() + "_" + chain;
            BindInfo bindInfo = new BindInfo(key, setter);
            List<BindInfo> bindList;
            if (bindDic.TryGetValue(key, out bindList))
            {
                foreach (BindInfo info in bindList)
                {
                    Action<object> action;
                    if (info.BindType == EBindType.Action && info.Action.TryGetTarget(out action) && action == setter)
                    {
                        throw new Exception("绑定出错！重复绑定！");
                    }
                }
                bindList.Add(bindInfo);
            }
            else
            {
                bindList = new List<BindInfo>();
                bindList.Add(bindInfo);
                bindDic.Add(key, bindList);
            }
            if (useForInit)
            {
                Type hostType = host.GetType();
                PropertyInfo hostPropertyInfo = hostType.GetProperty(chain, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                setter(hostPropertyInfo.GetValue(host));
            }
        }
        static public void OnValueChange(object host, string chain, object value)
        {
            string key = host.GetHashCode() + "_" + chain;
            List<BindInfo> bindList;
            if (bindDic.TryGetValue(key, out bindList))
            {
                foreach (BindInfo info in bindList.ToArray())
                {
                    SetValue(info, value);
                }
            }
        }

        static void SetValue(BindInfo info, object value)
        {
            if (info.BindType == EBindType.Field)
            {
                object site;
                if(info.Site.TryGetTarget(out site))
                {
                    Type siteType = site.GetType();
                    FieldInfo siteFieldInfo = siteType.GetField(info.Prop, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    siteFieldInfo.SetValue(site, value);
                }
                else
                {
                    bindDic[info.Key].Remove(info);
                }
            }
            else if (info.BindType == EBindType.Property)
            {
                object site;
                if (info.Site.TryGetTarget(out site))
                {
                    Type siteType = site.GetType();
                    PropertyInfo sitePropertyInfo = siteType.GetProperty(info.Prop, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    sitePropertyInfo.SetValue(site, value);
                }
                else
                {
                    bindDic[info.Key].Remove(info);
                }
            }
            else if (info.BindType == EBindType.Action)
            {
                Action<object> action;
                if(info.Action.TryGetTarget(out action))
                {
                    action(value);
                }
                else
                {
                    bindDic[info.Key].Remove(info);
                }
            }
        }

        static EBindType CheckSite(object site, string prop)
        {
            if (site == null)
            {
                return EBindType.Unknow;
            }
            Type type = site.GetType();
            PropertyInfo propertyInfo = type.GetProperty(prop, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            if (propertyInfo != null)
            {
                return EBindType.Property;
            }
            FieldInfo fieldInfo = type.GetField(prop, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            if (fieldInfo != null)
            {
                return EBindType.Field;
            }
            return EBindType.Unknow;
        }

        static bool CheckHost(object host, string chain)
        {
            if (host == null)
            {
                return false;
            }
            Type type = host.GetType();
            PropertyInfo propertyInfo = type.GetProperty(chain, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            if (propertyInfo != null && propertyInfo.GetMethod != null && propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic && propertyInfo.GetCustomAttribute<BindEnableAttribute>() != null)
            {
                return true;
            }
            return false;
        }

        static public void ClearAllBind()
        {
            bindDic.Clear();
        }
    }
}
