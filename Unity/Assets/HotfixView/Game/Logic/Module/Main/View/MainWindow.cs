/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
//--------******Alphas*NameSpace******--------//start

//--------******Alphas*NameSpace******--------//end

namespace Logic
{
    public partial class MainWindow : UIWindowForm
    {
        public MainWindowView view;
        public MainWindow() : base(MainWindowView.PackageName, MainWindowView.ResName)
        {
            BindItemExtension();
        }
        protected override void OnInit()
        {
            base.OnInit();
            view = GuiComponent as MainWindowView;
//--------******Alphas*OnInit******--------//start

//--------******Alphas*OnInit******--------//end
            OnInit_Supplement();
        }
        public static void BindItemExtension()
        {
            UIObjectFactory.SetPackageItemExtension(MainWindowView.URL, typeof(MainWindowView));
            //调用子节点BindItemExtension()
        }
        protected override void OnBindEvent()
        {
            view.m_powerBtn.onClick.Add(OnClickpowerBtn);
            view.m_headiconBtn.onClick.Add(OnClickheadiconBtn);
            view.m_SoldierBtn.onClick.Add(OnClickSoldierBtn);
            view.m_pull.onClick.Add(OnClickpull);
            view.m_down.onClick.Add(OnClickdown);
            view.m_bt_bg1.onClick.Add(OnClickbt_bg1);
            view.m_bt_bg2.onClick.Add(OnClickbt_bg2);
            view.m_bt_bg3.onClick.Add(OnClickbt_bg3);
            view.m_bt_bg4.onClick.Add(OnClickbt_bg4);
            view.m_bt_bg5.onClick.Add(OnClickbt_bg5);
            OnBindEvent_Supplement();
        }
//--------******Alphas*CostomFuntion******--------//start

//--------******Alphas*CostomFuntion******--------//end
    }
}