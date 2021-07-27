using Plugins.HubObject.Property;
using UnityEngine;

namespace HubObject.Doors.Data
{
    public class CustomPivot : DataProperty
    {
        public Transform Pivot => _pivot;
        [SerializeField] private Transform _pivot;
    }
}