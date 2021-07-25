using Plugins.HubObject;
using UnityEngine;

namespace HubObject
{
    public class Actor : MonoBehaviour
    {
        public BloodSystem BloodSystem => _bloodSystem != null ? _bloodSystem : _bloodSystem = new BloodSystem();
        public GeneralContainer GeneralContainer => _generalContainer;
        public ComponentShell ComponentShell => _componentShell;
        
        [SerializeField] private GeneralContainer _generalContainer;
        [SerializeField] private ComponentShell _componentShell;

        private BloodSystem _bloodSystem;
    }
}
