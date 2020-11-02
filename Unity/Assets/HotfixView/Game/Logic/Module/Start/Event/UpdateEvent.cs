/***
 * 开始下载。
 */

using UnityEngine;
using ET;
using FairyGUI;

namespace Logic
{
    public class UpdateBeginEvent : AEvent<EventIdType.UpdateBegin>
    {

        public override async ETTask Run(EventIdType.UpdateBegin a)
        {
        }
    }

    public class UpdateFinishEvent : AEvent<EventIdType.UpdateFinish>
    {

        public override async ETTask Run(EventIdType.UpdateFinish a)
        {


        }

    }
}