using System;
using System.Collections;
using Infrastructure;
using Services.Sound.Sound2Ds;
using UnityEngine;

namespace Services.Sound
{
    internal class AudioObserver
    {
        private readonly AudioSource _source;
        private readonly ISound2D _sound;
        private readonly ICoroutineRunner _coroutineRunner;
        public event Action<AudioSource, AudioObserver> End;
        
        public AudioObserver(AudioSource source, ISound2D sound, ICoroutineRunner coroutineRunner)
        {
            _source = source;
            _sound = sound;
            _coroutineRunner = coroutineRunner;
        }

        public void Start()
        {
            SetSettingSource();
            StartPlay();
            StartTimer();
        }

        private void StartTimer() => _coroutineRunner.StartCoroutine(Timer(_sound.CountLoop * _sound.Clip.length));

        private void StartPlay() => _source.Play();

        private void SetSettingSource()
        {
            _source.volume = _sound.Volume.Get();
            _source.priority = _sound.Priority;
            _source.pitch = _sound.Pitch.Get();
            _source.clip = _sound.Clip;
            _source.outputAudioMixerGroup = _sound.Group;
            _source.loop = _sound.CountLoop > 1;
        }

        private IEnumerator Timer(float time)
        {
            yield return new WaitForSeconds(time);
            End?.Invoke(_source, this);
        }
    }
}