/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
//--------******Alphas*NameSpace******--------//start

//--------******Alphas*NameSpace******--------//end

namespace Logic
{
    public partial class GameCoinWindow : UIWindowForm
    {
        public GameCoinWindowView view;
        public GameCoinWindow() : base(GameCoinWindowView.PackageName, GameCoinWindowView.ResName)
        {
            BindItemExtension();
        }
        protected override void OnInit()
        {
            base.OnInit();
            view = GuiComponent as GameCoinWindowView;
//--------******Alphas*OnInit******--------//start

//--------******Alphas*OnInit******--------//end
            OnInit_Supplement();
        }
        public static void BindItemExtension()
        {
            UIObjectFactory.SetPackageItemExtension(GameCoinWindowView.URL, typeof(GameCoinWindowView));
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