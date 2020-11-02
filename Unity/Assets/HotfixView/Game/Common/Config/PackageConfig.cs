namespace Logic
{
    /// <summary>
    /// 包的相关配置，出完包后，这里的数据就是固定的
    /// </summary>
    public class PackageConfig
    {
        public string PackageName; //包名
        public uint CodeVer;//包版本号
        public string ResVer;//包中自带的资源版本
        public uint PackageID;//包ID
        public bool HasExpansion;//是否有分包
        public string Hash;//包的hash值
        public string ZipFile= "resource.zip";//内部解压包名称
        public string UpdateHost;//更新地址
        public uint SDKType = 0;//
    }
}