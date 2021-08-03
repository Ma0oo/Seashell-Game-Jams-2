using System;
using HabObjects.Actors.Component.Enemy;
using UnityEngine;

namespace StateMachines.Enemy.States
{
    public class MoveToTarget : EnemyState
    {
        [SerializeField] private FollowPlayer _followPlayer;
        
        public override void On() => _followPlayer.enabled = true;

        public override void Off() => _followPlayer.enabled = false;

        public override void Step()
        {
            
        }
    }
}