using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public enum ResourceType
    {
        None = 0,
        Text = 1,
    }
    public class RemoteVersion
    {
        public string androidLinkurl;
        public string iosLinkurl;
        public bool maxCodeIsTip = false;
        public int maxCodeVersion;
        public string maxCodeVersionMSG;
        public int minCodeVersion;
        public string minCodeVersionMSG;
    }
    public class FileVersionInfo
    {
        public string File;
        public string MD5;
        public long Size;
        public ResourceType resourceType;
    }

    public class VersionConfig : Object
    {
        public int Version;

        public long TotalSize;

       // [BsonIgnore]
        public Dictionary<string, FileVersionInfo> FileInfoDict = new Dictionary<string, FileVersionInfo>();

        public override void EndInit()
        {
            base.EndInit();

            foreach (FileVersionInfo fileVersionInfo in this.FileInfoDict.Values)
            {
                this.TotalSize += fileVersionInfo.Size;
            }
        }
        public void Update(string key, FileVersionInfo _VersionConfig)
        {
            if (FileInfoDict.ContainsKey(key))
            {
                FileInfoDict[key].File = _VersionConfig.File;
                FileInfoDict[key].MD5 = _VersionConfig.MD5;
                FileInfoDict[key].Size = _VersionConfig.Size;
                FileInfoDict[key].resourceType = _VersionConfig.resourceType;
            }
            else
            {
                FileInfoDict.Add(key, new FileVersionInfo
                {
                    File = _VersionConfig.File,
                    MD5 = _VersionConfig.MD5,
                    Size = _VersionConfig.Size,
                    resourceType = _VersionConfig.resourceType,
                });
            }
        }
    }
}