
namespace ET
{
    [Facade]
    public abstract class Facade<T> : Entity, IFacade where T : Entity, new()
    {
        public virtual ETTask Run()
        {
            throw new System.NotImplementedException();
        }

        public virtual void StartUp()
        {
            throw new System.NotImplementedException();
        }
        public static T Instance
        {
            get
            {
                T _instance = Game.Scene.GetComponent<T>();
                if (_instance == null)
                {
                    _instance = Game.Scene.AddComponent<T>();
                }
                return _instance;
            }
        }
    }
}
