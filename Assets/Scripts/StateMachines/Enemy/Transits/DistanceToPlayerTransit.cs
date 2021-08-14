using System;
using HabObjects;
using Infrastructure;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace StateMachines.Enemy.Transits
{
    [AddComponentMenu(Transit.PathEnemy+"Distance To Player")]
    public class DistanceToPlayerTransit : Transit
    {
        [SerializeField] private float _distanceTrigger;
        [SerializeField] private bool _inverted;

        [Header("Gizmos")] 
        [SerializeField] private Color _colorSphere = Color.red;
        
        [DI(DIConstID.PlayerId)]private Actor _player;
        
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