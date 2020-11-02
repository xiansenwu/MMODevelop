using FairyGUI;
using System.Collections.Generic;

namespace Logic
{
    public class UIWindowForm : UIContainerForm
    {
        public UIWindowForm()
            : base()
        {

        }
        public UIWindowForm(GComponent skin)
            : base(skin)
        {
        }

        public UIWindowForm(string packageName, string resName, bool isResources = false)
            : base(packageName, resName, isResources)
        {
        }

        /// <summary>
        /// 是否转换场景时关闭该窗口(功能暂未实现)
        /// </summary>
        public bool IsChangeSceneClose { set; get; }
        public UIGroup mGroup = UIGroup.Common;

        protected void Close()
        {
            UIComponent.Instance.HideWindow(this);
        }

        protected override void __onInit()
        {
            GuiComponent.SetHome(UIHideRoot);
            GuiComponent.MakeFullScreen();
            base.__onInit();
        }

        public virtual void Oncall(string[] param)
        {

        }
    }
}
