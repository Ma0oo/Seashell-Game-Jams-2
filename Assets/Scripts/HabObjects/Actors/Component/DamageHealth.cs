using HabObjects.Actors.Component.Interfaces;
using HabObjects.Actors.Signals;
using UnityEngine;

namespace HabObjects.Actors.Component
{
    public class DamageHealth : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private HealthAbs _healthAbs;

        private void Awake() => _actor.BloodSystem.Track<FinallyDamage>(OnFinallyDamage);

        private void OnFinallyDamage(FinallyDamage @event) => _healthAbs.Damage(@event.Damage);
    }
}