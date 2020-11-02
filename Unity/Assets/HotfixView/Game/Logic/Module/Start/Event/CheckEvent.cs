/***
 * 开始下载。
 */

using UnityEngine;
using ET;
using FairyGUI;

namespace Logic
{
    public class CheckBeginEvent : AEvent<EventIdType.CheckBegin>
    {
        public override async ETTask Run(EventIdType.CheckBegin a)
        {
            
        }
    }
    public class CheckFinishEvent : AEvent<EventIdType.CheckFinish>
    {
        public override async ETTask Run(EventIdType.CheckFinish a)
        {
            
        }
    }
}