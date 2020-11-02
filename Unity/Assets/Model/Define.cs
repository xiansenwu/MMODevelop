namespace ET
{
	public static class Define
	{
        //是否运行Bundle模式
        public static bool IsRunBundle = false;
        //是否远程下载
        public static bool IsRemoteDown = false;

#if UNITY_EDITOR && !ASYNC
		public static bool IsAsync = false;
#else
        public static bool IsAsync = true;
#endif

#if UNITY_EDITOR
        public static bool IsEditorMode = true;
#else
		public static bool IsEditorMode = false;
#endif

#if DEVELOPMENT_BUILD
		public static bool IsDevelopmentBuild = true;
#else
		public static bool IsDevelopmentBuild = false;
#endif

	}
}