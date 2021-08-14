using System;
using UnityEngine;

namespace StateMachines.Enemy
{
    public abstract class State : MonoBehaviour
    {
        public const string PathGeneral = "1 StateMachine/";
        public const string PathRoom = "1 StateMachine/Room/State/";
        
        [SerializeField] private Transit[] _transits = new Transit[0];

        private void Awake()
        {
            Off();
            SetActiveTransit(false);
        }

        public abstract void On();
        public abstract void Off();
        public abstract void Step();

        public void SetActiveTransit(bool active)
        {
            foreach (var transit in _transits) transit.enabled = active;
        }

        public bool CanTransit(out State state)
        {
            state = null;
            bool result = false;
            if (_transits == null || _transits.Length == 0)
                return result;
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