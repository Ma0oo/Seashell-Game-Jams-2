using System;
using HubObject.Actors.Signals;
using UnityEngine;

namespace HubObject.Actors.Component
{
    public class DamageApllayer : MonoBehaviour
    {
        [SerializeField] private Actor _actor;

        private void Awake() => _actor.BloodSystem.Track<Damaged>(OnDamage);

        private void OnDamage(Damaged e) => _actor.BloodSystem.Fire(new FinallyDamage(e.Value));
    }
}