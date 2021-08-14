using System;
using System.Collections;
using HabObjects.Actors.Signals;
using UnityEngine;

namespace HabObjects.Actors.Component
{
    public class DamageApllayer : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private float _delay = 0;

        private void OnEnable() => _actor.BloodSystem.Track<Damaged>(OnDamage);

        private void OnDisable() => _actor.BloodSystem.Untrack<Damaged>(OnDamage);

        private void OnDamage(Damaged e)
        {
            _actor.BloodSystem.Fire(new FinallyDamage(e.Value));
            StartCoroutine(Delay(_delay));
        }

        private IEnumerator Delay(float time)
        {
            enabled = false;
            yield return new WaitForSeconds(_delay);
            enabled = true;
        }
    }
}