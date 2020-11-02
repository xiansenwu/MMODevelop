using ET;
using MongoDB.Bson.Serialization.Attributes;

namespace Logic
{
    [BsonIgnoreExtraElements]//低版本的协议需要能够反 序列化高版本的内容
    public class AccountInfo : Entity
    {
        //用户名
        public string Account { get; set; }

        //密码
        public string Password { get; set; }
    }
}
