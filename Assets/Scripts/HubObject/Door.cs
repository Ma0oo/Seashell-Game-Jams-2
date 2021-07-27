using Plugins.HubObject;
using UnityEngine;

namespace HubObject
{
    public class Door : MonoBehaviour
    {
        public GeneralContainer DataContainer => _container;
        public ComponentShell ComponentShell => _componentShell;
        public BloodSystem BloodSystem => _bloodySystem != null ? _bloodySystem : _bloodySystem = new BloodSystem();

        [SerializeField] private GeneralContainer _container;
        [SerializeField] private ComponentShell _componentShell;
        
        private BloodSystem _bloodySystem;
    }
}