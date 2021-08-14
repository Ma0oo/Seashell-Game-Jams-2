using System;
using System.Collections.Generic;
using System.Linq;
using HabObjects.Actors.Data;
using HabObjects.Actors.Signals;
using HabObjects.Rooms.Signals;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace HabObjects.Rooms.Component
{
    public class CounterMonsterInRoom : MonoBehaviour
    {
        public event Action AllMonsterDead;
        
        [SerializeField] private Room _room;

        private List<Actor> _liveMonster = new List<Actor>();
        private List<Actor> _deadMonster = new List<Actor>();
        
        [DIC]
        private void Init() => _room.BloodSystem.Track<ActorSpawnedInRoom>(OnMonsterSpawnedInRoom);

        private void OnMonsterSpawnedInRoom(ActorSpawnedInRoom obj)
        {
            if(obj.SpawnedToThisRoom != _room)
                return;
            if(!obj.Monster.GeneralContainer.GetOrNull<DungeonMonsterFlag>())
                return;

            _liveMonster.Add(obj.Monster);
            obj.Monster.BloodSystem.Track<ActorHasDead>(OnDeadActor);
            
        }

        private void OnDeadActor(ActorHasDead obj)
        {
            if(_liveMonster.Remove(obj._actorDead))
                _deadMonster.Add(obj._actorDead);
            
            if(_liveMonster.Count==0)
                AllMonsterDead?.Invoke();
        }

        private void OnDestroy()
        {
            //foreach (var actor in _liveMonster.Union(_deadMonster)) if(actor) Destroy(actor.gameObject);
        }
    }
}