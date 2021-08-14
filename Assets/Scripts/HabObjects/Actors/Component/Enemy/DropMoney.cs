using System;
using HabObjects.Actors.Signals;
using HabObjects.Dungeons.Component;
using HabObjects.Rooms.Signals;
using Plugins.HabObject.Customizable.Attributs;
using Plugins.HabObject.DIContainer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HabObjects.Actors.Component.Enemy
{
    [CustomizableComponent("Компонент дроп золота", 0)]
    public class DropMoney : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private CurrentRoomMonster _currentRoomMonster;
        
        [Min(0)][SerializeField][TRangeInt("Минимальная награда", 0, 1000, new []{1, 5, 10, 50})] 
        private int _minAward;
        [Min(0)][SerializeField][TRangeInt("Максимальная награда", 0, 1000, new []{1, 5, 10, 50})]
        private int _maxAward;

        [DI] private FactoryMoney _factoryMoney;


        private void Awake() => _actor.BloodSystem.Track<ActorHasDead>(OnActorDead);

        private void OnActorDead(ActorHasDead obj)
        {
            _factoryMoney.CreateAt(
                Random.Range(_minAward, _maxAward),
                _actor.transform.position,
                x=>x.transform.SetParent(_currentRoomMonster.Value.transform));
        }
    }
}