using StateMachines.Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace StateMachines
{
    [AddComponentMenu(State.PathGeneral+"EventState")]
    public class UnityEventState : State
    {
        [SerializeField] private UnityEvent _onEvent;
        [SerializeField] private UnityEvent _offEvent;
        
        public override void On() => _onEvent?.Invoke();

        public override void Off() => _offEvent?.Invoke();

        public override void Step()
        {
        }
    }
}