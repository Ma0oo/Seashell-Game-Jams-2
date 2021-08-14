using System;
using HabObjects;
using HabObjects.Actors.Signals;
using UnityEngine;

namespace Mechanics.ParticleSpawner
{
    public class ParticleOnHealthUpdate : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private ParticleSystem _damage;
        [SerializeField] private ParticleSystem _dead;

        private void Awake()
        {
            _actor.BloodSystem.Track<ActorHasDead>(x=>Instantiate(_dead, _actor.transform.position, Quaternion.identity));
            _actor.BloodSystem.Track<FinallyDamage>(x=>Instantiate(_damage, _actor.transform.position, Quaternion.identity));
        }
    }
}