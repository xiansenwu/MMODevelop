using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphas
{
    public class APData<T>
    {
        protected T oldData;
        protected T data;
        public void Set(T _data)
        {
            if(oldData == null || !oldData.Equals(_data))
            {
                data = _data;
                OnChange();
            }
            
        }
        public T Get()
        {
            return data;
        }
        public bool IsChange()
        {
            if (data == null && oldData == null)
                return false;
            if (oldData == null || !oldData.Equals(data))
            {
                return true;
            }
            return false;
        }
        protected void CancerChangeStat()
        {
            oldData = data;
        }
        protected virtual void OnChange()
        {
            CancerChangeStat();
        }
    }
}
