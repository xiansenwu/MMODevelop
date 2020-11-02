namespace Logic
{
    public enum EventSource
    {
        login = 1,
        reLogin = 2,
        fastLogin = 3,

    }

    namespace EventIdType
    {

        

        public struct CheckBegin { }
        public struct CheckFinish { }
        public struct UpdateBegin
        {
        }
        public struct UpdateFinish { }
        public struct UnpackBegin { }
        public struct UnpackFinish { }
        public struct LoadingBegin { }
        public struct LoadingFinish { }
        public struct StartEngine { }
        /// <summary>
        /// 进入登录场景
        /// </summary>
        public struct EnterLoginSceneEvent { }
        /// <summary>
        /// 登录完成
        /// </summary>
        public struct LoginFinishEvent { }
        /// <summary>
        /// 进入角色场景
        /// </summary>
        public struct EnterRoleSceneEvent { }
        /// <summary>
        /// 选角完成
        /// </summary>
        public struct RoleFinishEvent { }
        /// <summary>
        /// 退出角色登录状态完成
        /// </summary>
        public struct OutRoleLoginEvent { }
        /// <summary>
        /// 进入主场景
        /// </summary>
	    public struct EnterMainSceneEvent { }
        /// <summary>
        /// 进入地区场景
        /// </summary>
	    public struct EnterRegionSceneEvent { }


        /// <summary>
        /// 数据更变
        /// </summary>
	    public struct DataChangeEvent { }


    }
}