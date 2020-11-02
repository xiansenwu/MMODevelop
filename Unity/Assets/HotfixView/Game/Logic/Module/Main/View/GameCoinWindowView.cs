/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Logic
{
    public partial class GameCoinWindowView : GComponent
    {
        public GTextField m_woodLabel;
        public GTextField m_foodLabel;
        public GTextField m_goldLabel;
        public static string PackageName = "Main";
        public static string ResName = "GameCoin_Window";
        public const string URL = "ui://svwf6wyrg4d44e";

        public static GameCoinWindowView CreateInstance()
        {
            return (GameCoinWindowView)UIPackage.CreateObject(PackageName, ResName);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            this.fairyBatching = true;

            m_woodLabel = (GTextField)GetChild("woodLabel");
            m_foodLabel = (GTextField)GetChild("foodLabel");
            m_goldLabel = (GTextField)GetChild("goldLabel");
        }
    }
}