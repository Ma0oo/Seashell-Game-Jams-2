using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services.Sound.Volume
{
    [Serializable]
    public class VolumeSo : IVolume
    {
        [SerializeField] private bool isRandom;
        [Range(0,1f)] [SerializeField] private float _min = 1;
        [Range(0,1f)] [SerializeField] private float _max = 1;


        public float Get() => isRandom ? Random.Range(_min, _max) : _min;
    }
}