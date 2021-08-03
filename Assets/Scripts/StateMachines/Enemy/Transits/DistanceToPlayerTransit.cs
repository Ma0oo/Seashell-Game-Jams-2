using System;
using HabObjects;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace StateMachines.Enemy.Transits
{
    public class DistanceToPlayerTransit : Transit
    {
        public override EnemyState Target => _targetState;
        
        [SerializeField] private EnemyState _targetState;
        [SerializeField] private float _distanceTrigger;
        [SerializeField] private bool _inverted;

        [Header("Gizmos")] 
        [SerializeField] private Color _colorSphere = Color.red;
        
        private Actor _player;
        
        private void Awake() => _player = DiServices.MainContainer.ResolveSingle<Actor>(BootStrapGameScene.PlayerId);

        public override bool CanTransit()
        {
            bool result = Vector3.Distance(transform.position, _player.transform.position) < _distanceTrigger;
            if (_inverted) result = !result;
            return result;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _colorSphere;
            Gizmos.DrawWireSphere(transform.position, _distanceTrigger);
        }
    }
}