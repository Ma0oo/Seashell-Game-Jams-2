using Plugins.HabObject;
using UnityEngine;

namespace HabObjects
{
    public class Item : Mechanics.Interfaces.HabObject
    {
        public override BloodSystem BloodSystem => _bloodSystem != null ? _bloodSystem : _bloodSystem = new BloodSystem();
        public override ComponentShell ComponentShell => _componentShell;
        public override GeneralContainer GeneralContainer => _generalContainer;

        [SerializeField] private ComponentShell _componentShell;
        [SerializeField] private GeneralContainer _generalContainer;
        
        private BloodSystem _bloodSystem;
    }
}