using Alphas;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
namespace Logic
{
    public class LoginModelSystem : AwakeSystem<LoginModel>
    {
        public override void Awake(LoginModel self)
        {
            self.mAccountText = PlayerPrefsHelper.Get<string>(LoginModel.AccountTextSaveKey);
            self.mPasswordText = PlayerPrefsHelper.Get<string>(LoginModel.PasswordTextSaveKey);
        }
    }
    public class LoginModel :Entity
    {
        public static string AccountTextSaveKey = "AccountText";
        public static string PasswordTextSaveKey = "PasswordText";
        public ServerInfo mLastServerInfo;
        public ServerInfo mCurServerInfo;

        public string mAccountText;
        public string mPasswordText;
    }
    [BsonNoId]
    public class ServerInfo
    {
        
        //[BsonElement("id")]
        public string id;
        public string name;
        public string serverid;
        public string number;
        public string ip;
        public string port;
        public string state;
    }

}
