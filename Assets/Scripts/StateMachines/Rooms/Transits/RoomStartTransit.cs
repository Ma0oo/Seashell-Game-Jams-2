using System;
using HabObjects;
using HabObjects.Dungeons.Component;
using StateMachines.Enemy;
using UnityEngine;

namespace StateMachines.Rooms.Transits
{
    [AddComponentMenu(Transit.PathRoom+"StartRoom")]
    public class RoomStartTransit : Transit
    {
        [SerializeField] private Room _room;
        
        private bool _ready;

        private void OnEnable() => _room.BloodSystem.Track<StartRoom>(OnStart);

        private void OnDisable()
        {
            _room.BloodSystem.Untrack<StartRoom>(OnStart);
            _ready = false;
        }

        private void OnStart(StartRoom obj) => _ready = true;

        public override bool CanTransit() => _ready;
    }
}