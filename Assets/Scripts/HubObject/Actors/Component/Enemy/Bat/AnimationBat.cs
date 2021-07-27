using HubObject.Actors.Signals;
using HubObject.Actors.Signals.Bat;
using UnityEngine;

namespace HubObject.Actors.Component.Enemy.Bat
{
    public class AnimationBat : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private Animator _animator;
        
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Damaged = Animator.StringToHash("Damaged");
        private static readonly int CloseAttack = Animator.StringToHash("CloseAttack");

        private float _currentHealth = 100;

        private void Awake() => _actor.BloodSystem.Track<HealthUpdated>(e => _currentHealth = e.Current);

        private void OnEnable()
        {
            _actor.BloodSystem.Track<ActorDeaded>(OnDead);
            _actor.BloodSystem.Track<FinallyDamage>(OnDamaged);
            _actor.BloodSystem.Track<StartBatCloserAttackPlayer>(OnCloseAttack);
        }

        private void OnCloseAttack(StartBatCloserAttackPlayer obj) => _animator.SetTrigger(CloseAttack);

        private void OnDisable()
        {
            _actor.BloodSystem.Untrack<ActorDeaded>(OnDead);
            _actor.BloodSystem.Untrack<FinallyDamage>(OnDamaged);
            _actor.BloodSystem.Untrack<StartBatCloserAttackPlayer>(OnCloseAttack);
        }

        private void OnDamaged(FinallyDamage e)
        {
            if(e.Damage>0 && e.Damage<_currentHealth) 
                _animator.SetTrigger(Damaged);
        }

        private void OnDead(ActorDeaded e)
        {
            _animator.SetTrigger(Dead);
            enabled = false;
        }
    }
}