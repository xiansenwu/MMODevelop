/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
//--------******Alphas*NameSpace******--------//start

//--------******Alphas*NameSpace******--------//end

namespace Logic
{
    public partial class LoginDebugWindow : UIWindowForm
    {
        public LoginDebugWindowView view;
        public LoginDebugWindow() : base(LoginDebugWindowView.PackageName, LoginDebugWindowView.ResName)
        {
            BindItemExtension();
        }
        protected override void OnInit()
        {
            base.OnInit();
            view = GuiComponent as LoginDebugWindowView;
//--------******Alphas*OnInit******--------//start

//--------******Alphas*OnInit******--------//end
            OnInit_Supplement();
        }
        public static void BindItemExtension()
        {
            UIObjectFactory.SetPackageItemExtension(LoginDebugWindowView.URL, typeof(LoginDebugWindowView));
            //调用子节点BindItemExtension()
        }
        protected override void OnBindEvent()
        {
            view.m_loginBtn.onClick.Add(OnClickloginBtn);
            view.m_menaLoginBtn.onClick.Add(OnClickmenaLoginBtn);
            view.m_serverListBox.onChanged.Add(OnChangedserverListBox);
            OnBindEvent_Supplement();
        }
//--------******Alphas*CostomFuntion******--------//start

//--------******Alphas*CostomFuntion******--------//end
    }
}