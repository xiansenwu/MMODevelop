using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Logic
{
    public class LoginLogicComponentAwakeSystem : AwakeSystem<LoginLogicComponent>
    {
        public override void Awake(LoginLogicComponent t)
        {
            t.Awake();
        }
    }
    public class LoginLogicComponent : Entity
    {
        public void Awake()
        {

        }
        public async ETTask Run()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }
}
