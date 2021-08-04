using Mechanics.Interfaces;
using Plugins.HabObject;
using UnityEngine;

namespace HabObjects
{
    public class Dungeon : HabObject
    {
        public override BloodSystem BloodSystem => _bloodSystem ??= new BloodSystem();
        public override ComponentShell ComponentShell => _componentShell;
        public override GeneralContainer GeneralContainer => _generalContainer;

        private BloodSystem _bloodSystem;
        [SerializeField] private ComponentShell _componentShell;
        [SerializeField] private GeneralContainer _generalContainer;
    }
}