﻿using UnityEngine;

namespace ET
{
    public static class UnitFactory
    {
        public static Unit Create(Entity domain, long id)
        {
	        Unit unit = EntityFactory.CreateWithId<Unit>(domain, id);
	        
	        //unit.AddComponent<TurnComponent>();

	        Game.EventSystem.Publish(new EventType.AfterUnitCreate() {Unit = unit}).Coroutine();
	        
	        UnitComponent unitComponent = Game.Scene.GetComponent<UnitComponent>();
            unitComponent.Add(unit);
            return unit;
        }
    }
}