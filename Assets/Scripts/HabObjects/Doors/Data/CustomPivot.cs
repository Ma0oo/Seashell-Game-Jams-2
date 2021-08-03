using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Doors.Data
{
    public class CustomPivot : DataProperty
    {
        public Transform Pivot => _pivot;
        [SerializeField] private Transform _pivot;
    }
}