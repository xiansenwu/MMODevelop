using FairyGUI;
using ET;
using UnityEngine;

using System.Collections.Generic;
using System;

namespace Logic
{
    public class UIContainerForm : UIForm
    {
        public UIContainerForm()
            : base()
        {

        }
        public UIContainerForm(GComponent skin)
            : base(skin)
        {
            _guiComponent = skin;
        }

        public UIContainerForm(string packageName, string resName, bool isResources = false)
            : base(packageName, resName, isResources)
        {
        }

        protected GComponent _guiComponent = null;
        public GComponent GuiComponent
        {
            get
            {
                if (_guiComponent == null)
                {
                    if (UISkin is GComponent)
                    {
                        _guiComponent = UISkin as GComponent;
                    }
                    else
                    {
                        throw new Exception(string.Format("UIContainerForm {0} must has a GComponent skin", this.Name));
                    }
                }
                return _guiComponent;
            }
        }


        public override void Dispose()
        {
            _guiComponent = null;
            base.Dispose();
        }

    }
}
