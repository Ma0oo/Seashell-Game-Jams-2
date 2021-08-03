using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Actors.Data
{
    public class PivotCloseWeapon : DataProperty
    {
        public Transform Value => _pivot;
        [SerializeField] private Transform _pivot;
    }
}