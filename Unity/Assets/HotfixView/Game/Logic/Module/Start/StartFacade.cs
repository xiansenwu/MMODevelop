using ET;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    public class StartFacade : Facade<StartFacade>
    {
        public override void StartUp()
        {
            
        }
        public override async ETTask Run()
        {
            Log.Debug("StartFacade Run");

            UIComponent.Instance.ShowWindow<StartWindow>();


            Game.Scene.AddComponent<GlobalConfigComponent>();
            //await CheckLogic.Instance.Run();

            Game.Scene.AddComponent<BundleDownloaderComponent>();
            //解压
            UnpackLogicComponent _UnpackLogicComponent = Game.Scene.AddComponent<UnpackLogicComponent>();
            await _UnpackLogicComponent.Run();
            Game.Scene.RemoveComponent<UnpackLogicComponent>();
            //更新
            //UpdateLogicComponent _UpdateLogicComponent = Game.Scene.AddComponent<UpdateLogicComponent>();
            //await _UpdateLogicComponent.Run();
            //Game.Scene.RemoveComponent<UpdateLogicComponent>();

            LoadLogicComponent _LoadLogicComponent = Game.Scene.AddComponent<LoadLogicComponent>();
            await _LoadLogicComponent.Run();
            Game.Scene.RemoveComponent<LoadLogicComponent>();


            //检查大版本更新
            CheckLogicComponent _CheckLogicComponent = Game.Scene.AddComponent<CheckLogicComponent>();
            _CheckLogicComponent.Run().Coroutine();
            //Game.Scene.RemoveComponent<CheckLogicComponent>();

            //init 所有模块
            Dictionary<Type, IFacade> allFacades = Game.EventSystem.GetFacades();
            foreach (IFacade facade in allFacades.Values)
            {
                if (facade is StartFacade)
                    continue;
                Game.Scene.AddComponent(facade as Entity);
                facade.StartUp();
            }
            foreach (IFacade facade in allFacades.Values)
            {
                if (facade is StartFacade)
                    continue;
                await facade.Run();
            }
            UIComponent.Instance.ShowWindow<LoginDebugWindow>();
        }
    }
}