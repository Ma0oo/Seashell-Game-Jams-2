using System;
using HubObject.Actors.Component.Interfaces;
using HubObject.Actors.Signals;
using UnityEngine;

namespace HubObject.Actors.Component
{
    public class DamageHealth : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private HealthAbs _healthAbs;

        private void Awake() => _actor.BloodSystem.Track<FinallyDamage>(OnFinallyDamage);

        private void OnFinallyDamage(FinallyDamage @event) => _healthAbs.Damage(@event.Damage);
    }
}