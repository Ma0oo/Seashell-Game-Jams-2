using System;
using UnityEngine;

namespace Plugins.HabObject
{
    public abstract class HabObject : MonoBehaviour
    {
        public abstract BloodSystem BloodSystem { get; }
        public abstract ComponentShell ComponentShell { get; }
        public abstract GeneralContainer GeneralContainer { get; }

        private void OnDestroy() => BloodSystem.Clear();
    }
}