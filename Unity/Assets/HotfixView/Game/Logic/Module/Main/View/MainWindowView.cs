/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Logic
{
    public partial class MainWindowView : GComponent
    {
        public Controller m_c1;
        public GButton m_powerBtn;
        public GButton m_headiconBtn;
        public GTextField m_powerTxt;
        public GLoader m_headbg;
        public GGroup m_head;
        public GButton m_SoldierBtn;
        public GGroup m_bom;
        public GButton m_pull;
        public GButton m_down;
        public GButton m_bt_bg1;
        public GButton m_bt_bg2;
        public GButton m_bt_bg3;
        public GButton m_bt_bg4;
        public GButton m_bt_bg5;
        public static string PackageName = "Main";
        public static string ResName = "Main_Window";
        public const string URL = "ui://svwf6wyrp8430";

        public static MainWindowView CreateInstance()
        {
            return (MainWindowView)UIPackage.CreateObject(PackageName, ResName);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            this.fairyBatching = true;

            m_c1 = GetController("c1");
            m_powerBtn = (GButton)GetChild("powerBtn");
            m_headiconBtn = (GButton)GetChild("headiconBtn");
            m_powerTxt = (GTextField)GetChild("powerTxt");
            m_headbg = (GLoader)GetChild("headbg");
            m_head = (GGroup)GetChild("head");
            m_SoldierBtn = (GButton)GetChild("SoldierBtn");
            m_bom = (GGroup)GetChild("bom");
            m_pull = (GButton)GetChild("pull");
            m_down = (GButton)GetChild("down");
            m_bt_bg1 = (GButton)GetChild("bt_bg1");
            m_bt_bg2 = (GButton)GetChild("bt_bg2");
            m_bt_bg3 = (GButton)GetChild("bt_bg3");
            m_bt_bg4 = (GButton)GetChild("bt_bg4");
            m_bt_bg5 = (GButton)GetChild("bt_bg5");
        }
    }
}