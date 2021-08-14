using System;
using UnityEngine;

namespace StateMachines.Enemy
{
    public class StateMachines : MonoBehaviour
    {
        [SerializeField] private State _firstState;

        private State _currentState;

        private void Start() => ChangeState(_firstState);

        private void Update()
        {
            _currentState.Step();
            if( _currentState.CanTransit(out var enemyState))
                ChangeState(enemyState);
        }

        private void ChangeState(State newState)
        {
            if (_currentState)
            {
                _currentState.Off();
                _currentState.SetActiveTransit(false);
            }

            _currentState = newState;
            _currentState.SetActiveTransit(true);
            _currentState.On();
        }
    }
}