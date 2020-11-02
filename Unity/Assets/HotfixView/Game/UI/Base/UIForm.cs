using FairyGUI;
using ET;
using UnityEngine;

using System.Collections.Generic;
using System;

namespace Logic
{
    public class UIForm : Entity
    {
        public static UIContainerForm UIRoot = new UIContainerForm(GRoot.inst);
        public static GComponent UIHideRoot = new GComponent() { gameObjectName = "UIHide" };
        public UIForm()
        {
            
        }
        public UIForm(GObject skin)
        {
            _uiSkin = skin;
            if(skin.packageItem == null)
            {
                Name = skin.name;
                ResName = skin.name;
                PackageName = skin.name;
            }
            else
            {
                Name = skin.packageItem.name;
                ResName = skin.packageItem.name;
                PackageName = skin.packageItem.owner.name;
            }
            __onInit();
        }

        public UIForm(string packageName, string resName, bool isResources = false)
        {
            PackageName = packageName;
            ResName = resName;
            Name = resName;
            IsResources = isResources;
        }
        public string Name { get; set; }
        public string PackageName { get; protected set; }
        public string ResName { get; protected set; }

        public bool IsShowing { get; protected set; }
        public bool IsResources = false;
        protected GObject _uiSkin = null;
        public GObject UISkin {
            get
            {
                if(_uiSkin == null)
                {
                    if (IsResources)
                    {
                        UIPackageHelp.AddPackageFromResources(PackageName);
                        _uiSkin = UIPackage.CreateObject(PackageName, ResName);
                    }
                    else
                    {
                        _uiSkin = UIPackageHelp.CreateObject(PackageName, ResName);
                    }
                    __onInit();
                }
                return _uiSkin;
            }
        }
        

        private object _uiData = null;
        public object UIData
        {
            get
            {
                return _uiData;
            }
            set
            {
                _uiData = value;
                if(IsShowing)
                    OnSetData(_uiData);
            }
        }

        protected virtual void __onInit()
        {
            UISkin.displayObject.onAddedToStage.Add(__onShown);
            UISkin.displayObject.onRemovedFromStage.Add(__onHide);
            OnInit();
            OnBindEvent();
        }

        protected virtual async ETTask __doShowAnimationAsync()
        {
            await OnDoShowAnimation();
            OnShow();
        }

        protected virtual void __onShown()
        {
            IsShowing = true;
            UISkin.visible = true;
            OnSetData(UIData);
            __doShowAnimationAsync().Coroutine();
        }

        protected virtual async ETTask __doHideAnimationAsync(bool isDispose = false)
        {
            await OnDoHideAnimation();
            if(UIParent == null || isDispose)
            {
                UIRoot.GuiComponent.RemoveChild(UISkin, isDispose);
            }
            else
            {
                __onHide();
            }
            if(isDispose)
            {
                Dispose();
                
            }
        }

        protected virtual void __onHide()
        {
            IsShowing = false;
            UISkin.visible = false;
            OnHide();
        }

        public UIContainerForm UIParent { get; internal set; }

        public virtual void Show()
        {
            if(UISkin == null)
            {
                return;
            }
            UIContainerForm container = UIParent == null ? UIRoot : UIParent;
            if (IsShowing)
            {
                container.GuiComponent.SetChildIndex(UISkin, container.GuiComponent.numChildren);
                OnShow();
            }
            else if(UISkin.parent == null)
            {
                container.GuiComponent.AddChild(UISkin);
            }
            else
            {
                __onShown();
            }
        }

        public virtual void Hide(bool isDispose = false)
        {
            __doHideAnimationAsync(isDispose).Coroutine();
        }

        protected virtual async ETTask OnDoHideAnimation()
        {
            await ETTask.CompletedTask;
        }

        protected virtual async ETTask OnDoShowAnimation()
        {
            await ETTask.CompletedTask;
        }

        protected virtual void OnInit()
        {
            
        }

        protected virtual void OnBindEvent()
        {

        }

        protected virtual void OnUnBindEvent()
        {

        }

        protected virtual void OnShow()
        {
            
        }

        protected virtual void OnHide()
        {

        }

        protected virtual void OnSetData(object userData)
        {

        }

        protected virtual void OnDispose()
        {
            if (IsResources == false)
                UIPackageHelp.RemovePackage(PackageName);

        }

        public override void Dispose()
        {
            
            UIParent = null;
            _uiSkin = null;
            _uiData = null;
             OnUnBindEvent();
             OnDispose();
            base.Dispose();
        }
    }
}
