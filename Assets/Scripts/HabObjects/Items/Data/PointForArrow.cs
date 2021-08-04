using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Items.Components
{
    public class PointForArrow : DataProperty
    {
        public Transform Value => _point;
        [SerializeField] private Transform _point;
    }
}