/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Logic
{
    public partial class WorldToolWindowView : GComponent
    {
        public GButton m_centerPoint;
        public GTextField m_pointTxt;
        public static string PackageName = "World";
        public static string ResName = "WorldTool_Window";
        public const string URL = "ui://qvviy45lrmhr4d";

        public static WorldToolWindowView CreateInstance()
        {
            return (WorldToolWindowView)UIPackage.CreateObject(PackageName, ResName);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            this.fairyBatching = true;

            m_centerPoint = (GButton)GetChild("centerPoint");
            m_pointTxt = (GTextField)GetChild("pointTxt");
        }
    }
}