using Plugins.HubObject.Property;
using UnityEngine;

namespace HubObject.Actors.Data
{
    public class MaxSpeedData : DataProperty
    {
        public float Value => _maxSpeed;
        [Min(0.1f)] [SerializeField] private float _maxSpeed;
    }
}