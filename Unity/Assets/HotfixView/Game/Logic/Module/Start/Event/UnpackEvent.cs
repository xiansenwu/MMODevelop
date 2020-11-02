/***
 * 开始下载。
 */

using UnityEngine;
using ET;
using FairyGUI;

namespace Logic
{

    public class UnpackBeginEvent : AEvent<EventIdType.UnpackBegin>
    {
        public override async ETTask Run(EventIdType.UnpackBegin a)
        {
            
        }
    }
    public class UnpackFinishEvent : AEvent<EventIdType.UnpackFinish>
    {
        public override async ETTask Run(EventIdType.UnpackFinish a)
        {
            //throw new System.NotImplementedException();
        }
    }
}