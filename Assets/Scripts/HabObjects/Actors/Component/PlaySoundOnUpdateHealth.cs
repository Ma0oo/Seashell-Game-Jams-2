using System;
using HabObjects.Actors.Signals;
using Plugins.HabObject.DIContainer;
using Services.Sound;
using Services.Sound.Sound2Ds;
using UnityEngine;

namespace HabObjects.Actors.Component
{
    public class PlaySoundOnUpdateHealth : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private Sound2DSO _soundDamage;
        [SerializeField] private Sound2DSO _soundDead;

        [DI] private SoundSystem _soundSystem;
        
        private void OnEnable()
        {
            _actor.BloodSystem.Track<FinallyDamage>(OnDamage);
            _actor.BloodSystem.Track<ActorHasDead>(OnDead);
        }

        private void OnDisable()
        {
            _actor.BloodSystem.Untrack<FinallyDamage>(OnDamage);
            _actor.BloodSystem.Untrack<ActorHasDead>(OnDead);
        }

        private void OnDead(ActorHasDead obj)
        {
            _soundSystem.Play(_soundDead);
            enabled = false;
        }

        private void OnDamage(FinallyDamage obj) => _soundSystem.Play(_soundDamage);
    }
}