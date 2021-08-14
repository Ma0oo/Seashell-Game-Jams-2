using System.Collections;
using HabObjects.Actors.Signals;
using HabObjects.Actors.Signals.Bat;
using Infrastructure;
using Plugins.HabObject.Customizable.Attributs;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace HabObjects.Actors.Component.Enemy.Bat
{
    [CustomizableComponent("Настройка летучий мыши", 0)]
    public class BatCloseAttackerPlayer : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [TRangeFloat("Задержка между аттак", 0, 10, new []{1f})]
        [Min(0.05f)][SerializeField] private float _delayBetweenAttack;
        [TRangeFloat("Задержка перед первой аттакой", 0, 10, new []{1f})]
        [Min(0.05f)][SerializeField] private float _delayOnEbable;
        [TRangeFloat("Урон атаки", 0, 100, new []{1f})]
        [Min(0)][SerializeField] private float _damage;

        [DI(DIConstID.PlayerId)]private Actor _player;
        private Coroutine _actionAttack;
        
        private void Awake() => _actor.BloodSystem.Track<BatAttackMoment>(OnAttackMoment);

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