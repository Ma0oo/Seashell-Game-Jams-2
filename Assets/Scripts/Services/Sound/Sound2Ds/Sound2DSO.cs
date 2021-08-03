using System;
using Services.Sound.Pitch;
using Services.Sound.Volume;
using UnityEngine;
using UnityEngine.Audio;

namespace Services.Sound.Sound2Ds
{
    [CreateAssetMenu]
    public class Sound2DSO : ScriptableObject, ISound2D
    {
        public AudioClip Clip => _clip;
        public int Priority => _priority;
        public IVolume Volume => _volume;
        public IPitch Pitch => _pitch;
        public AudioMixerGroup Group => _group;
        public int CountLoop => _countLoop;

        [SerializeField] private AudioClip _clip;
        [SerializeField] private VolumeSo _volume;
        [SerializeField] private PitchSo _pitch;
        [Range(0,256)][SerializeField] private int _priority = 128;
        [Min(1)][SerializeField] private int _countLoop = 1;
        [SerializeField] private AudioMixerGroup _group;

    }
}