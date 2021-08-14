using HabObjects;
using HabObjects.Dungeons.Component;
using StateMachines.Enemy;
using UnityEngine;

namespace StateMachines.Rooms.Transits
{
    [AddComponentMenu(Transit.PathRoom+"StopRoom")]
    public class StopRoomTransit : Transit
    {
        [SerializeField] private Room _room;
        
        private bool _ready;

        private void OnEnable() => _room.BloodSystem.Track<StopRoom>(OnStop);

        private void OnDisable()
        {
            _room.BloodSystem.Untrack<StopRoom>(OnStop);
            _ready = false;
        }

        private void OnStop(StopRoom obj) => _ready = true;
        
        public override bool CanTransit()
        {
            return _ready;
        }
    }
}