using HabObjects;
using HabObjects.Rooms.Component;
using StateMachines.Enemy;
using UnityEngine;

namespace StateMachines.Rooms.Transits
{
    [AddComponentMenu(Transit.PathRoom+"All Monster Dead")]
    public class RoomAllMonsterDeadTransit : Transit
    {
        [SerializeField] private Room _room;
        [SerializeField] private CounterMonsterInRoom counterMonster;
        
        private bool _ready;

        private void OnEnable() => counterMonster.AllMonsterDead += OnMonsterDead;

        private void OnDisable()
        {
            counterMonster.AllMonsterDead += OnMonsterDead;
            _ready = false;
        }

        private void OnMonsterDead() => _ready = true;

        public override bool CanTransit() => _ready;
    }
}