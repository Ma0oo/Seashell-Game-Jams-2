using Services.Sound.Pitch;
using Services.Sound.Volume;
using UnityEngine;
using UnityEngine.Audio;

namespace Services.Sound
{
    public interface ISound2DBase
    {
        public AudioClip Clip { get; }
        public int Priority { get; }
        public IVolume Volume { get; }
        public IPitch Pitch { get; }
        public AudioMixerGroup Group { get; }
    }
}