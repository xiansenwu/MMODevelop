/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
//--------******Alphas*NameSpace******--------//start

//--------******Alphas*NameSpace******--------//end

namespace Logic
{
    public partial class WorldWindow : UIWindowForm
    {
        public WorldWindowView view;
        public WorldWindow() : base(WorldWindowView.PackageName, WorldWindowView.ResName)
        {
            BindItemExtension();
        }
        protected override void OnInit()
        {
            base.OnInit();
            view = GuiComponent as WorldWindowView;
//--------******Alphas*OnInit******--------//start

//--------******Alphas*OnInit******--------//end
            OnInit_Supplement();
        }
        public static void BindItemExtension()
        {
            UIObjectFactory.SetPackageItemExtension(WorldWindowView.URL, typeof(WorldWindowView));
            //调用子节点BindItemExtension()
        }
        protected override void OnBindEvent()
        {
            OnBindEvent_Supplement();
        }
//--------******Alphas*CostomFuntion******--------//start

//--------******Alphas*CostomFuntion******--------//end
    }
}