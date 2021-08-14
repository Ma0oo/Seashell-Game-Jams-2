using System;
using HabObjects;
using HabObjects.Actors.Component;
using Plugins.HabObject.DIContainer;
using Services.Sound;
using Services.Sound.Sound2Ds;
using UnityEngine;

namespace Mechanics
{
    public class PlaySoundOnFinishTimer : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private Sound2DSO _sound;

        [DI] private SoundSystem _soundSystem;

        private void Awake()
        {
            _actor.BloodSystem.Track<TimerFinish>(x=>_soundSystem.Play(_sound));
        }
    }
}