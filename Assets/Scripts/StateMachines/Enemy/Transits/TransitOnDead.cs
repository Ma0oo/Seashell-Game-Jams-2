using System;
using HabObjects;
using HabObjects.Actors.Signals;
using UnityEngine;

namespace StateMachines.Enemy.Transits
{
    [AddComponentMenu(Transit.PathEnemy+"Transit dead")]   
    public class TransitOnDead : Transit
    {
        [SerializeField] private Actor _actor;

        private bool result = false;
        
        private void Awake() => _actor.BloodSystem.Track<ActorHasDead>(x=> result = true);

        public override bool CanTransit() => result;
    }
}