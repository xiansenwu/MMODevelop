using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public partial class PlayerInfo :Entity
    {
        public string NickName = "";
        public int Level = 0;
        public int Goldens = 0;
        public int Diamods = 0;

        public Msg_PlayerInfo ToMsgPlayerInfo()
        {
            Msg_PlayerInfo _msg = new Msg_PlayerInfo();
            _msg.NickName = this.NickName;
            _msg.Level = this.Level;
            _msg.Goldens = this.Goldens;
            _msg.Diamods = this.Diamods;

            return _msg;
        }
        public static PlayerInfo Create(Msg_PlayerInfo _msg)
        {
            return null;
        }
    }
}
