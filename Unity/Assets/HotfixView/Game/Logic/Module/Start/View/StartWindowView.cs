/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Logic
{
    public partial class StartWindowView : GComponent
    {
        public Controller m_state;
        public GImage m_bg;
        public GProgressBar m_bar;
        public GTextField m_load;
        public GTextField m_update;
        public static string PackageName = "Start";
        public static string ResName = "Start_Window";
        public const string URL = "ui://sg1qxvpxeplx0";

        public static StartWindowView CreateInstance()
        {
            return (StartWindowView)UIPackage.CreateObject(PackageName, ResName);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            this.fairyBatching = true;

            m_state = GetController("state");
            m_bg = (GImage)GetChild("bg");
            m_bar = (GProgressBar)GetChild("bar");
            m_load = (GTextField)GetChild("load");
            m_update = (GTextField)GetChild("update");
        }
    }
}