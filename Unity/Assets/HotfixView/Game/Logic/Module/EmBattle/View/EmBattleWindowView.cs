/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Logic
{
    public partial class EmBattleWindowView : GComponent
    {
        public Controller m_unlockCtr;
        public Controller m_selectCtr;
        public Controller m_defenseCtr;
        public WindowTitle m_frame;
        public static string PackageName = "EmBattle";
        public static string ResName = "EmBattle_Window";
        public const string URL = "ui://4ex9q1xxrdnt0";

        public static EmBattleWindowView CreateInstance()
        {
            return (EmBattleWindowView)UIPackage.CreateObject(PackageName, ResName);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            this.fairyBatching = true;

            m_unlockCtr = GetController("unlockCtr");
            m_selectCtr = GetController("selectCtr");
            m_defenseCtr = GetController("defenseCtr");
            m_frame = (WindowTitle)GetChild("frame");
        }
    }
}