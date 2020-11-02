namespace Logic
{
    public class DeubgConfig
    {
        public string LogLevel;//日志等级DEBUG = 1, INFO = 2, WARN = 3, ERROR = 4, END = 5
        public string LogOption= "11111";//日志开关5位分别为isDownload，isNormal，isBattle，isTPQueue，isPlot 1为打开0为关闭

        public bool IsShowButton = true; //是否显示日志按钮

        //更新标记0:不更新，1:普通更新，2:远程更新，3:普通更新跟远程更新
        public byte UpdateMode = 0;
    }
}