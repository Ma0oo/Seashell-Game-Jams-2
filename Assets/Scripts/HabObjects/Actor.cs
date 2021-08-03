using Plugins.HabObject;
using UnityEngine;

namespace HabObjects
{
    public class Actor : Mechanics.Interfaces.HabObject
    {
        public override BloodSystem BloodSystem => _bloodSystem != null ? _bloodSystem : _bloodSystem = new BloodSystem();
        public override GeneralContainer GeneralContainer => _generalContainer;
        public override ComponentShell ComponentShell => _componentShell;
        
        [SerializeField] private GeneralContainer _generalContainer;
        [SerializeField] private ComponentShell _componentShell;

        private BloodSystem _bloodSystem;
    }
}
