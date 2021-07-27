using Plugins.HubObject;
using UnityEngine;

namespace HubObject
{
    public class Room : MonoBehaviour
    {
        public BloodSystem BloodSystem => _bloodSystem != null ? _bloodSystem : _bloodSystem = new BloodSystem();
        public ComponentShell ComponentShell => _componentShell;
        public GeneralContainer PropertyContainer => _propertyContainer;
        
        [SerializeField] private ComponentShell _componentShell;
        [SerializeField] private GeneralContainer _propertyContainer;

        private BloodSystem _bloodSystem;
    }
}