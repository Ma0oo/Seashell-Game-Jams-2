using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Actors.Data
{
    public class MaxSpeedData : DataProperty
    {
        public float Value => _maxSpeed;
        [Min(0.1f)] [SerializeField] private float _maxSpeed;
    }
}