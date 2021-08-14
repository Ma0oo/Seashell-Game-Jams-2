using HabObjects.Actors.Component.Enemy.Bat;
using UnityEngine;

namespace StateMachines.Enemy.States.Bat
{
    public class BatAttackPlayerState : State
    {
        [SerializeField] private BatCloseAttackerPlayer batCloseAttackerPlayer;
        
        public override void On() => batCloseAttackerPlayer.enabled = true;

        public override void Off() => batCloseAttackerPlayer.enabled = false;

        public override void Step()
        {
        }
        
    }
}