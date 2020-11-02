/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
//--------******Alphas*NameSpace******--------//start

//--------******Alphas*NameSpace******--------//end

namespace Logic
{
    public partial class EmBattleWindow : UIWindowForm
    {
        public EmBattleWindowView view;
        public EmBattleWindow() : base(EmBattleWindowView.PackageName, EmBattleWindowView.ResName)
        {
            BindItemExtension();
        }
        protected override void OnInit()
        {
            base.OnInit();
            view = GuiComponent as EmBattleWindowView;
//--------******Alphas*OnInit******--------//start

//--------******Alphas*OnInit******--------//end
            OnInit_Supplement();
        }
        public static void BindItemExtension()
        {
            UIObjectFactory.SetPackageItemExtension(EmBattleWindowView.URL, typeof(EmBattleWindowView));
            //调用子节点BindItemExtension()
            UIObjectFactory.SetPackageItemExtension(WindowTitle.URL, typeof(WindowTitle));
        }
        protected override void OnBindEvent()
        {
            view.m_frame.m_closeBtn.onClick.Add(OnClickCloseBtn);
            view.m_frame.m_homeBtn.onClick.Add(OnClickHomeBtn);
            OnBindEvent_Supplement();
        }
        private void OnClickCloseBtn(EventContext context)
        {
            UIComponent.Instance.HideWindow(this);
        }
        private void OnClickHomeBtn(EventContext context)
        {
            UIComponent.Instance.HideAllWindows();
        }
//--------******Alphas*CostomFuntion******--------//start

//--------******Alphas*CostomFuntion******--------//end
    }
}