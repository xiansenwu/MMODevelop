using System.Collections.Generic;

using Alphas;
using ET;
using FairyGUI;

using UnityEngine;

namespace Logic
{
    public class OperaComponentAwakeSystem : AwakeSystem<OperaComponent>
    {
        public override void Awake(OperaComponent self)
        {
            self.Awake();
           
        }
    }

    public class OperaComponentUpdateSystem : UpdateSystem<OperaComponent>
    {
        public override void Update(OperaComponent self)
        {
            self.Update();
        }
    }


    public class OperaComponent: Entity
    {
        public static OperaComponent Instance;

        public LayerMask mask;

        public void Awake()
        {
            Instance = this;
            mask = 1 << (LayerMask.NameToLayer("Map"));
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                
                if(Physics.Raycast(ray, out hit, 1000, mask.value)){
                    Debug.LogError(hit.transform.name);


                }
            }
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

        }
    }
}
