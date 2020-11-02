using System;
using System.Collections.Generic;
using ET;
using FairyGUI;
using Alphas;
namespace Logic
{
    
    public class UiComponentAwakeSystem : AwakeSystem<UIComponent>
    {
        public override void Awake(UIComponent self)
        {
            self.Awake();
        }
    }

    public class UIComponent :  Entity
    {
        List<UIWindowForm> _showWindows;
        Dictionary<Type, UIWindowForm> _TypeWindows; //一般游戏只会出现一次的ui都放这里  tips new出来
        List<UIWindowForm> tmpDeleteWin = new List<UIWindowForm>();
        public static UIComponent Instance;
        public const int designResolutionWight = 640;
        public const int designResolutionHeight = 1136;
        public void Awake()
        {
            _showWindows = new List<UIWindowForm>();
            _TypeWindows = new Dictionary<Type, UIWindowForm>();
            Instance = this;

            //FairyGUI全局设置
            UIObjectFactory.SetLoaderExtension(typeof(ExtendGLoader));
            GRoot.inst.SetContentScaleFactor(designResolutionWight, designResolutionHeight, UIContentScaler.ScreenMatchMode.MatchWidth);

            
        }
        private void HideAction( int uIActionFlag)
        {
            tmpDeleteWin.Clear();
            if (_showWindows.Count > 0)
            {
                for (int i = _showWindows.Count - 1; i >= 0; i--)
                {
                    if (BitHelper.AndIsTrue((int)_showWindows[i].mGroup, uIActionFlag) == true)
                    {
                        if (BitHelper.AndIsTrue(uIActionFlag, (int)(UIFlag.Hide| UIFlag.Close)) == true)
                        {
                            tmpDeleteWin.Add(_showWindows[i]);
                        }
                        //处理上一个控件
                        if (BitHelper.AndIsTrue(uIActionFlag, (int)UIFlag.Last) == true)
                        {
                            break;
                        }
                    }
                }
            }
            foreach (UIWindowForm delwin in tmpDeleteWin)
            {
                HideWindow(delwin, uIActionFlag);
            }
        }
        #region ShowWindow
        public void ShowWindow<T>(object userData = null, UIActionFlag flag = UIActionFlag.HideLastCommon) where T : UIWindowForm, new()
        {
            UIWindowForm _win;
            Type _type = typeof(T);
            if (!_TypeWindows.ContainsKey(_type))
            {
                _win = EntityFactory.Create<T>(this,false);//this.AddComponent<T>();//new T();
                _TypeWindows.Add(typeof(T), _win);
            }
            _win = _TypeWindows[_type];
            ShowWindow(_win, userData, (int)flag);
        }
        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="win"></param>
        /// <param name="userData"></param>
        /// <param name="isActive">是否会被切换</param>
        public void ShowWindow(UIWindowForm win, object userData = null, int uIActionFlag = (int)UIActionFlag.HideLastCommon)
        {

            HideAction(uIActionFlag);
            try
            {
                _showWindows.Remove(win);
                _showWindows.Add(win);
                win.UIData = userData;
                if(win.Parent != this)
                    win.Parent = this;
                win.Show();
            }
            catch(Exception ex)
            {
                Log.Error(ex);
            }
            Log.Debug(string.Format("show {0} !", win.Name));
        }
        #endregion
        #region HideWindow
        public void HideWindow<T>(int uIActionFlag = (int)UIActionFlag.HideLastCommon)
        {
            Type _type = typeof(T);
            HideWindow(_type, uIActionFlag);
        }
        public void HideWindow(Type _type, int uIActionFlag = (int)UIActionFlag.HideLastCommon)
        {
            UIWindowForm _win = _TypeWindows[_type];
            if (_win == null)
                return;
            HideWindow(_win, uIActionFlag);

        }
        public void HideWindow(UIWindowForm win, int uIActionFlag = (int)UIActionFlag.HideLastCommon)
        {
            Type _type = win.GetType();
            bool isDispose = BitHelper.AndIsTrue(uIActionFlag, (int)UIFlag.Close);
            if (isDispose)
            {
                _TypeWindows.Remove(_type);
                //RemoveComponent(win);
            }
            _showWindows.Remove(win);
            win.Hide(isDispose);
            
            Log.Debug(string.Format("hide {0} !", win.Name));
        }
        #endregion
        public UIWindowForm GetTopWindow()
        {
           
            if (_showWindows.Count > 0)
            {
                return _showWindows[_showWindows.Count - 1];
            }
            return null;
        }

        public void HideAllWindows(int uIActionFlag = (int)UIGroup.Common)
        {
            tmpDeleteWin.Clear();
            for (int i=0; i< _showWindows.Count; ++i)
            {
                if (BitHelper.AndIsTrue((int)_showWindows[i].mGroup, uIActionFlag) == true)
                {
                    tmpDeleteWin.Add(_showWindows[i]);
                }
            }
            foreach(UIWindowForm win in tmpDeleteWin)
            {
                HideWindow(win, uIActionFlag);
            }
        }

        public void HideWait()
        {
            GRoot.inst.CloseModalWait();
        }

        public void ShowWait()
        {
            GRoot.inst.ShowModalWait();
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            _showWindows.Clear();
            _TypeWindows.Clear();
            
        }
    }
}
