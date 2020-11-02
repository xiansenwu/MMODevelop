using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphas
{
    //ImmediatelyAPData
    public class ImAPData<T>:APData<T>
    {
        public delegate void DataChangeCallback(T oldData,T curData);
        private List<DataChangeCallback> callbacks = new List<DataChangeCallback>();
        protected override void OnChange()
        {
            foreach(DataChangeCallback _callback in callbacks)
            {
                _callback.Invoke(oldData,data);
            }
            base.OnChange();
        }
        public void AddEvent(DataChangeCallback _callback)
        {
            callbacks.Remove(_callback);
            callbacks.Add(_callback);
        }
        public void RemoveEvent(DataChangeCallback _callback)
        {
            callbacks.Remove(_callback);
        }
    }
}
