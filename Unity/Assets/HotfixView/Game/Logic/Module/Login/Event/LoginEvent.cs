using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;

namespace Logic
{

    public class LoginFinishEvent : AEvent<ET.EventType.LoginFinish>
    {
        public override async ETTask Run(ET.EventType.LoginFinish a)
        {
            try
            {
                UIComponent.Instance.HideAllWindows((int)UIActionFlag.CloseAll);
                UIComponent.Instance.ShowWindow<MainWindow>();
                MapHelper.EnterMapAsync(a.ZoneScene, "Map").Coroutine();
                //await Game.EventSystem.Publish(new EventIdType.EnterMainSceneEvent());
            }
            catch (Exception ex)
            {
                Log.Error("---------------ex " + ex.ToString());
            }

        }//run_end

    }//class_end
}