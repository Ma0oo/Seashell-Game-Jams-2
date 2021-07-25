using Plugins.HubObject.Property;
using UnityEngine;

namespace HubObject.Actors.Data
{
    public class PivotCloseWeapon : DataProperty
    {
        public Transform Value => _pivot;
        [SerializeField] private Transform _pivot;
    }
}