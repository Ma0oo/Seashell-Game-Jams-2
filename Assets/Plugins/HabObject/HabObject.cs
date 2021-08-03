using Plugins.HabObject;
using UnityEngine;

namespace Mechanics.Interfaces
{
    public abstract class HabObject : MonoBehaviour
    {
        public abstract BloodSystem BloodSystem { get; }
        public abstract ComponentShell ComponentShell { get; }
        public abstract GeneralContainer GeneralContainer { get; }
    }
}