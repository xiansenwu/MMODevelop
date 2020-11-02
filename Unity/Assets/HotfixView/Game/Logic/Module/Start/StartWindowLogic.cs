/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using System.Collections.Generic;

namespace Logic
{
    public partial class StartWindow : UIWindowForm
    {
        protected void OnInit_Supplement()
        {
            this.mGroup = UIGroup.AlwayBottom;
//             Game.EventSystem.RegisterEvent(EventIdType.UnpackBegin, UnpackBegin);
//             Game.EventSystem.RegisterEvent(EventIdType.UnpackFinish, UnpackFinish);
//             Game.EventSystem.RegisterEvent(EventIdType.UpdateBegin, UpdateBegin);
//             Game.EventSystem.RegisterEvent(EventIdType.UpdateFinish, UpdateFinish);
        }
        protected void OnBindEvent_Supplement()
        {
        }
        public override void Dispose()
        {
//             Game.EventSystem.RemoveEvent(EventIdType.UnpackBegin, UnpackBegin);
//             Game.EventSystem.RemoveEvent(EventIdType.UnpackFinish, UnpackFinish);
//             Game.EventSystem.RemoveEvent(EventIdType.UpdateBegin, UpdateBegin);
//             Game.EventSystem.RemoveEvent(EventIdType.UpdateFinish, UpdateFinish);

            base.Dispose();
        }
        public void UnpackBegin(List<object> objs)
        {
            view.m_state.selectedPage = "loadState";
        }
        public void UnpackFinish(List<object> objs)
        {
        }
        public void UpdateBegin(List<object> objs)
        {
            view.m_state.selectedPage = "updateState";
        }
        public void UpdateFinish(List<object> objs)
        {

        }
//--------******Alphas*OnBtnClickBind******--------//start





//--------******Alphas*OnBtnClickBind******--------//end
    }
}