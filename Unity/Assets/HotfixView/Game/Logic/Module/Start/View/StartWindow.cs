/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
//--------******Alphas*NameSpace******--------//start

//--------******Alphas*NameSpace******--------//end

namespace Logic
{
    public partial class StartWindow : UIWindowForm
    {
        public StartWindowView view;
        public StartWindow() : base(StartWindowView.PackageName, StartWindowView.ResName,true)
        {
            BindItemExtension();
        }
        protected override void OnInit()
        {
            base.OnInit();
            view = GuiComponent as StartWindowView;
//--------******Alphas*OnInit******--------//start

//--------******Alphas*OnInit******--------//end
            OnInit_Supplement();
        }
        public static void BindItemExtension()
        {
            UIObjectFactory.SetPackageItemExtension(StartWindowView.URL, typeof(StartWindowView));
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