using System;
using Factorys;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace HabObjects.Actors.Component.Enemy.Goblin
{
    public class Goblin : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private Actor _bombTemplate;

        [DI] private FactoryHabObject _factoryHabObject;

        public void SpawnBomb()
        {
            _factoryHabObject.Create(_bombTemplate, _actor.transform.position, _actor.ComponentShell.Get<CurrentRoomMonster>().Value).BloodSystem
                .Fire(new StartBomb());
        }
    }
}