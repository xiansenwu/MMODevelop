using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ET
{
    public interface IFacade
    {
        void StartUp();
        ETTask Run();
    }
}