using System;
using HubObject.Actors.Signals;
using UnityEngine;

namespace HubObject.Actors.Component.Enemy
{
    public class AnimationBat : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private Animator _animator;
        
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Damaged = Animator.StringToHash("Damaged");

        private float _currentHealth = 100;
        
        private void Awake() => _actor.BloodSystem.Track<HealthUpdated>(e => _currentHealth = e.Current);

        private void OnEnable()
        {
            _actor.BloodSystem.Track<Dead>(OnDead);
            _actor.BloodSystem.Track<FinallyDamage>(OnDamaged);
            
        }

        private void OnDisable()
        {
            _actor.BloodSystem.Untrack<Dead>(OnDead);
            _actor.BloodSystem.Untrack<FinallyDamage>(OnDamaged);
        }

        private void OnDamaged(FinallyDamage e)
        {
            if(e.Damage>0 && e.Damage<_currentHealth) 
                _animator.SetTrigger(Damaged);
        }

        private void OnDead(Dead e)
        {
            _animator.SetTrigger(Dead);
            enabled = false;
        }
    }
}