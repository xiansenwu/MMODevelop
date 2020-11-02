/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Logic
{
    public partial class LoginDebugWindowView : GComponent
    {
        public GButton m_loginBtn;
        public GButton m_menaLoginBtn;
        public GComboBox m_serverListBox;
        public GTextField m_ipText;
        public GTextField m_portText;
        public GTextInput m_AccoutInput;
        public static string PackageName = "Login";
        public static string ResName = "LoginDebug_Window";
        public const string URL = "ui://4p8n5d60cb7w0";

        public static LoginDebugWindowView CreateInstance()
        {
            return (LoginDebugWindowView)UIPackage.CreateObject(PackageName, ResName);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            this.fairyBatching = true;

            m_loginBtn = (GButton)GetChild("loginBtn");
            m_menaLoginBtn = (GButton)GetChild("menaLoginBtn");
            m_serverListBox = (GComboBox)GetChild("serverListBox");
            m_ipText = (GTextField)GetChild("ipText");
            m_portText = (GTextField)GetChild("portText");
            m_AccoutInput = (GTextInput)GetChild("AccoutInput");
        }
    }
}