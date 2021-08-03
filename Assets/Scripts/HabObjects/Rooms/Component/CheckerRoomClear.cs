using System.Collections.Generic;
using HabObjects.Actors.Signals;
using HabObjects.Rooms.Signals;
using UnityEngine;

namespace HabObjects.Rooms.Component
{
    public class CheckerRoomClear : MonoBehaviour
    {
        [SerializeField] private Room _room;

        private List<Actor> _enemys = new List<Actor>();
        
        private void OnEnable()
        {
            _room.BloodSystem.Track<DungeonMonsterSpawned>(OnDungeonMonsterSpawned);
        }

        private void OnDungeonMonsterSpawned(DungeonMonsterSpawned obj)
        {
            if(_enemys.Contains(obj.Monster))
                return;
            _enemys.Add(obj.Monster);
            obj.Monster.BloodSystem.Track<ActorDeaded>(OnMonsterDead);
        }

        private void OnMonsterDead(ActorDeaded obj)
        {
            obj._actorDead.BloodSystem.Untrack<ActorDeaded>(OnMonsterDead);
            if(!_enemys.Contains(obj._actorDead))
                return;
            _enemys.Remove(obj._actorDead);
            if(_enemys.Count==0)
                _room.BloodSystem.Fire(new RoomCleaned());
        }
    }
}