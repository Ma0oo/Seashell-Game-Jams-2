using Services.Sound.Sound2DLoops;
using UnityEngine;

namespace Services.Sound
{
    internal class SourceLoopObserver
    {
        private readonly ISound2DLoop _sound;
        private readonly AudioSource _sorce;

        public SourceLoopObserver(ISound2DLoop sound, AudioSource sorce)
        {
            _sound = sound;
            _sorce = sorce;

            _sorce.clip = _sound.Clip;
            _sorce.priority = _sound.Priority;
            _sorce.outputAudioMixerGroup = _sound.Group;
            _sorce.pitch = _sound.Pitch.Get();
            _sorce.volume = _sound.Volume.Get();
            _sorce.loop = true;
        }

        public void Play() => _sorce.Play();

        public AudioSource Stop()
        {
            _sorce.Stop();
            return _sorce;
        }
    }
}