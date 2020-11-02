namespace Logic
{
    public class StatisticsConfig
    {
        public string StsHostStartUp;//启动流程相关统计
        public string StsHostLogic;//系统功能统计
        public uint StsAppid = 16;
        public string StsSecret;
        public string StsApiInterface;
        public string StsUseInterface= "DeviceActive,RegisterProcess,Login,Register";
    }
}