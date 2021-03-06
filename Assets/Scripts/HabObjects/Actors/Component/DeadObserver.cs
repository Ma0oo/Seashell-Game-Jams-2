using System.Collections;
using HabObjects.Actors.Signals;
using UnityEngine;
using UnityEngine.Events;

namespace HabObjects.Actors.Component
{
    public class DeadObserver : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private UnityEvent _onDead;

        private void OnEnable() => _actor.BloodSystem.Track<HealthUpdated>(OnUpdateHealth);

        private void OnDisable() => _actor.BloodSystem.Untrack<HealthUpdated>(OnUpdateHealth);

        private void OnUpdateHealth(HealthUpdated obj)
        {
            if (HealthIsEmpty(obj.Current)) StartCoroutine(DeadDelay());
        }

        private static bool HealthIsEmpty(float current) => current == 0;

        private IEnumerator DeadDelay()
        {
            yield return new WaitForFixedUpdate();
            _actor.BloodSystem.Fire(new ActorHasDead(_actor));
            _onDead?.Invoke();
        }
    }
}