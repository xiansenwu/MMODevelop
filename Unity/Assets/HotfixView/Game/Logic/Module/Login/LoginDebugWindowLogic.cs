/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public partial class LoginDebugWindow : UIWindowForm
    {
        protected void OnInit_Supplement()
        {
            LoginFacade _LoginFacade = LoginFacade.Instance;
            LoginModel _LoginModel = _LoginFacade.GetComponent<LoginModel>();
            SetInputAccout(_LoginModel.mAccountText);
        }
        protected void OnBindEvent_Supplement()
        {
        }
//--------******Alphas*OnBtnClickBind******--------//start






        private void OnClickloginBtn(EventContext context)
        {
            
            LoginFacade _LoginFacade = LoginFacade.Instance;
            LoginModel _LoginModel = _LoginFacade.GetComponent<LoginModel>();
            ServerInfo serverInfo = _LoginModel.mCurServerInfo;
            _LoginModel.mAccountText = view.m_AccoutInput.text;
            _LoginModel.mPasswordText = "111111";
            _LoginModel.mLastServerInfo = _LoginModel.mCurServerInfo;
            LoginHelper.Login(Game.Scene, "127.0.0.1:10002", _LoginModel.mAccountText).Coroutine();
            //PlayerPrefsHelper.Save(_LoginModel.mLastServerInfo);
            PlayerPrefsHelper.Save(LoginModel.AccountTextSaveKey, _LoginModel.mAccountText);
            PlayerPrefsHelper.Save(LoginModel.PasswordTextSaveKey, _LoginModel.mPasswordText);
            //ET.Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("uires/uipackage/world_atlas1.unity3d");
        }
        private void OnClickmenaLoginBtn(EventContext context)
        {
            
            //ET.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("uires/uipackage/world_atlas1.unity3d");
            int a = 0;
            a++;
        }
        private void OnChangedserverListBox(EventContext context)
        {
            GComboBox gComboBox = context.sender as GComboBox;

        }



//--------******Alphas*OnBtnClickBind******--------//end

        private void SetInfo(ServerInfo serverInfo)
        {
            view.m_ipText.text = serverInfo.ip;
            view.m_portText.text = serverInfo.port;
        }
        private void SetInputAccout(string accout)
        {
            view.m_AccoutInput.text = accout;
        }
    }
}