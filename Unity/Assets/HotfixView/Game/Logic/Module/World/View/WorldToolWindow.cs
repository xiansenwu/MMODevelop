/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
//--------******Alphas*NameSpace******--------//start

//--------******Alphas*NameSpace******--------//end

namespace Logic
{
    public partial class WorldToolWindow : UIWindowForm
    {
        public WorldToolWindowView view;
        public WorldToolWindow() : base(WorldToolWindowView.PackageName, WorldToolWindowView.ResName)
        {
            BindItemExtension();
        }
        protected override void OnInit()
        {
            base.OnInit();
            view = GuiComponent as WorldToolWindowView;
//--------******Alphas*OnInit******--------//start

//--------******Alphas*OnInit******--------//end
            OnInit_Supplement();
        }
        public static void BindItemExtension()
        {
            UIObjectFactory.SetPackageItemExtension(WorldToolWindowView.URL, typeof(WorldToolWindowView));
            //调用子节点BindItemExtension()
        }
        protected override void OnBindEvent()
        {
            view.m_centerPoint.onClick.Add(OnClickcenterPoint);
            OnBindEvent_Supplement();
        }
//--------******Alphas*CostomFuntion******--------//start

//--------******Alphas*CostomFuntion******--------//end
    }
}