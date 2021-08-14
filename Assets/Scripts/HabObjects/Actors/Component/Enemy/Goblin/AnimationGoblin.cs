using System;
using HabObjects.Actors.Component.Enemy.Bat;
using HabObjects.Actors.Signals;
using HabObjects.Actors.Signals.Bat;
using UnityEngine;
using UnityEngine.AI;

namespace HabObjects.Actors.Component.Enemy.Goblin
{
    public class AnimationGoblin : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Damaged = Animator.StringToHash("Damaged");
        private static readonly int CloseAttack = Animator.StringToHash("HasAttack");

        private float _health = 100;

        private void Awake() => _actor.BloodSystem.Track<HealthUpdated>(x=>_health =x.Current);

        private void OnEnable()
        {
            _actor.BloodSystem.Track<ActorHasDead>(OnDead);
            _actor.BloodSystem.Track<FinallyDamage>(OnDamaged);
            _actor.BloodSystem.Track<GoblinAttackMoment>(OnAttack);
        }

        private void OnDisable()
        {
            _actor.BloodSystem.Untrack<ActorHasDead>(OnDead);
            _actor.BloodSystem.Untrack<FinallyDamage>(OnDamaged);
            _actor.BloodSystem.Untrack<GoblinAttackMoment>(OnAttack);
        }

        private void OnAttack(GoblinAttackMoment obj) => _animator.SetTrigger(Damaged);

        private void OnDamaged(FinallyDamage obj)
        {
            if(obj.Damage>0 && obj.Damage<_health)
                _animator.SetTrigger(Damaged);
        }

        private void Update()
        {
            _animator.SetFloat(Speed, Math.Abs(_agent.velocity.magnitude));
            _spriteRenderer.flipX = _agent.velocity.x < 0;
        }

        private void OnDead(ActorHasDead obj)
        {
            _animator.SetBool(Dead, true);
            enabled = false;
        }
    }
}