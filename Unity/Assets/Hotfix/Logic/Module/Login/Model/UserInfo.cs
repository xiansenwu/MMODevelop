using ET;
using MongoDB.Bson.Serialization.Attributes;

namespace Logic
{
    [BsonIgnoreExtraElements]
    public class UserInfo : Entity
    {
        // 昵称
        public string NickName { get; set; }

        // 等级
        public int Level { get; set; }

        // 金币
        public int Goldens { get; set; }

        // 钻石
        public int Diamods { get; set; }

    }

}
