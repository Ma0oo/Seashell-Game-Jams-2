using System;
using HabObjects.Actors.Signals;
using HabObjects.Actors.Signals.Bat;
using UnityEngine;
using UnityEngine.AI;

namespace HabObjects.Actors.Component.Enemy.Bat
{
    public class AnimationBat : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private NavMeshAgent _agent;

        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Damaged = Animator.StringToHash("Damaged");
        private static readonly int CloseAttack = Animator.StringToHash("CloseAttack");

        private float _currentHealth = 100;

        private void Awake() => _actor.BloodSystem.Track<HealthUpdated>(e => _currentHealth = e.Current);

        private void OnEnable()
        {
            _actor.BloodSystem.Track<ActorHasDead>(OnDead);
            _actor.BloodSystem.Track<FinallyDamage>(OnDamaged);
            _actor.BloodSystem.Track<StartBatCloserAttackPlayer>(OnCloseAttack);
        }

        private void OnDisable()
        {
            _actor.BloodSystem.Untrack<ActorHasDead>(OnDead);
            _actor.BloodSystem.Untrack<FinallyDamage>(OnDamaged);
            _actor.BloodSystem.Untrack<StartBatCloserAttackPlayer>(OnCloseAttack);
        }

        private void Update() => _spriteRenderer.flipX = _agent.velocity.x < 0;
        
        private void OnCloseAttack(StartBatCloserAttackPlayer obj) => _animator.SetTrigger(CloseAttack);


        private void OnDamaged(FinallyDamage e)
        {
            if(e.Damage>0 && e.Damage<_currentHealth) 
                _animator.SetTrigger(Damaged);
        }

        private void OnDead(ActorHasDead e)
        {
            _animator.SetTrigger(Dead);
            enabled = false;
        }
    }
}