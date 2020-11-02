using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public enum UIGroup
    {
        AlwayBottom = 0x0100, //如果不想区分太复杂那最底层的UI请使用这个
        Bg = 0x00200, //背景层 UI
        //AnimationUnder = 0x0030, //动画层
        Common = 0x0400, //普通层 UI
        //AnimationOn = 0x0050, // 动画层
        PopUI = 0x0800, //弹出层 UI
        Guide = 0x0F00, //新手引导层
        Const = 0x1000, //持续存在层 UI
        Toast = 0x2000, //对话框层 UI
        //Forward = 0x0A0, //最高UI层用来放置UI特效和模型
        AlwayTop = 0x4000, //如果不想区分太复杂那最上层的UI请使用这个
        All = AlwayBottom | Bg | Common | PopUI | Const|
            Guide  | Toast | AlwayTop,
    }
    public enum UIFlag
    {
        Hide = 0x0001,
        Show = 0x0002,
        Close = 0x0004,
        //UIGroup
        //0x0010 to 0x00F0
        //
        Last = 0x0010,
    }
    public enum UIActionFlag
    {
        None = 0,
        HideAllCommon = UIFlag.Hide | UIGroup.Common,
        HideLastCommon = UIFlag.Hide| UIFlag.Last| UIGroup.Common,
        CloseLastCommon = UIFlag.Close | UIFlag.Last | UIGroup.Common,
        CloseAll = UIFlag.Close | UIGroup.All,
    }
}
