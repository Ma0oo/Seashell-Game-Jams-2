using System;
using UnityEngine;

namespace StateMachines.Enemy
{
    public abstract class EnemyState : MonoBehaviour
    {
        [SerializeField] private Transit[] _transits;

        private void Awake() => Off();

        public abstract void On();
        public abstract void Off();
        public abstract void Step();

        public bool CanTransit(out EnemyState state)
        {
            state = null;
            bool result = false;
            foreach (var transit in _transits)
            {
                result = transit.CanTransit();
                if (result)
                {
                    state = transit.Target;
                    break;
                }
            }
            return result;
        }
    }
}