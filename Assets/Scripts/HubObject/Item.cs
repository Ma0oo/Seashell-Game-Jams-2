using Plugins.HubObject;
using UnityEngine;

namespace HubObject
{
    public class Item : MonoBehaviour
    {
        public BloodSystem BloodSystem => _bloodSystem != null ? _bloodSystem : _bloodSystem = new BloodSystem();
        public ComponentShell ComponentShell => _componentShell;
        public GeneralContainer GeneralContainer => _generalContainer;

        [SerializeField] private ComponentShell _componentShell;
        [SerializeField] private GeneralContainer _generalContainer;
        
        private BloodSystem _bloodSystem;
    }
}