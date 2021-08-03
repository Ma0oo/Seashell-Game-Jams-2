using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services.Sound.Pitch
{
    [Serializable]
    public class PitchSo : IPitch
    {
        [SerializeField] private bool _isRandom;

        [Range(-3,3f)][SerializeField] private float _min = 1;
        [Range(-3,3f)][SerializeField] private float _max = 1;

        public float Get() => _isRandom ? Random.Range(_min, _max) : _min;
    }
}