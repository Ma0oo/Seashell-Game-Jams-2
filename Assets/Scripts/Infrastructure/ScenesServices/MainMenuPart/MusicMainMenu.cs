using System;
using Plugins.HabObject.DIContainer;
using Services.Sound;
using Services.Sound.Sound2DLoops;
using UnityEngine;

namespace Infrastructure.ScenesServices.MainMenuPart
{
    public class MusicMainMenu : MonoBehaviour
    {
        [SerializeField] private SoundLoop2DSO _music;

        [DI] private SoundSystem _soundSystem;

        private void Awake() => Play();

        public void Play()
        {
            _soundSystem.Play(_music, SoundSystem.LoopAction.Start, out var transformSource);
            transformSource.position = Camera.main.transform.position;
        }

        public void Stop() => _soundSystem.Play(_music, SoundSystem.LoopAction.Stop, out var t);

    }
}