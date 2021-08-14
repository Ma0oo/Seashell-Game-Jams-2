using Plugins.HabObject;
using UnityEngine;

namespace HabObjects
{
    public class Door : HabObject
    {
        public override ComponentShell ComponentShell => _componentShell;
        public override GeneralContainer GeneralContainer => _container;
        public override BloodSystem BloodSystem => _bloodySystem != null ? _bloodySystem : _bloodySystem = new BloodSystem();

        [SerializeField] private GeneralContainer _container;
        [SerializeField] private ComponentShell _componentShell;
        
        private BloodSystem _bloodySystem;
    }
}