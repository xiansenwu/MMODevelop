using ET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    
    public class LoginFacade :Facade<LoginFacade>
    {
        public override void StartUp()
        {
            
        }
        public override async ETTask Run()
        {
            Log.Debug("LoginFacade Run");
            this.AddComponent<LoginLogicComponent>();
            this.AddComponent<LoginModel>();
        }
    }
}