/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Logic
{
    public partial class WindowTitle : GLabel
    {
        public GButton m_closeBtn;
        public GButton m_homeBtn;
        public static string PackageName = "Common";
        public static string ResName = "wndTitle_WindowTitle";
        public const string URL = "ui://s8oshc2db55su5";

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_closeBtn = (GButton)GetChild("closeBtn");
            m_homeBtn = (GButton)GetChild("homeBtn");
//--------******Alphas*OnInit******--------//start
            
//--------******Alphas*OnInit******--------//end
        }
//--------******Alphas*CostomFuntion******--------//start
        
//--------******Alphas*CostomFuntion******--------//end
    }
}