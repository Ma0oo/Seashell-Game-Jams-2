using System.Collections;
using HubObject.Actors.Signals;
using HubObject.Actors.Signals.Bat;
using Plugins.HubObject.GlobalSystem;
using UnityEngine;

namespace HubObject.Actors.Component.Enemy.Bat
{
    public class BatCloseAttackerPlayer : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [Min(0.05f)][SerializeField] private float _delayBetweenAttack;
        [Min(0.05f)][SerializeField] private float _delayOnEbable;
        [Min(0)][SerializeField] private float _damage;

        private Actor _player;
        private Coroutine _actionAttack;
        
        private void Awake()
        {
            _player = ServicesLocator.MainContainer.ResolveSingle<Actor>(BootStrapGameScene.PlayerId);
            _actor.BloodSystem.Track<BatAttackMoment>(OnAttackMoment);
        }

        private void OnAttackMoment(BatAttackMoment obj) => _player.BloodSystem.Fire(new Damaged(_damage));

        private void OnEnable() => _actionAttack = StartCoroutine(Attack());

        private void OnDisable() => StopCoroutine(_actionAttack);

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(_delayOnEbable);
            while (true)
            {
                _actor.BloodSystem.Fire(new StartBatCloserAttackPlayer());
                yield return new WaitForSeconds(_delayBetweenAttack);
            }
        }
    }
}