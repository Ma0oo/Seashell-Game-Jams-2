using System;
using Factorys;
using Plugins.HabObject;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace HabObjects.Actors.Component.Enemy
{
    public class SpawnSomeHubObject : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private HabObject _template;
        [SerializeField] private CurrentRoomMonster _currentRoomMonster;

        [DI] private FactoryHabObject _factoryHab;
        
        public void Spawn()
        {
            var instance = _factoryHab.Create(_template, _actor.transform.position);
            instance.transform.SetParent(_currentRoomMonster.Value.transform);
        }
    }
}