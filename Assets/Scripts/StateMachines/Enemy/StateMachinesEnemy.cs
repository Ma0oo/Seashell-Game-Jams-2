using System;
using UnityEngine;

namespace StateMachines.Enemy
{
    public class StateMachinesEnemy : MonoBehaviour
    {
        [SerializeField] private EnemyState _firstState;

        private EnemyState _currentState;

        private void Start() => ChangeState(_firstState);

        private void Update()
        {
            _currentState.Step();
            if( _currentState.CanTransit(out var enemyState))
                ChangeState(enemyState);
        }

        private void ChangeState(EnemyState newState)
        {
            if(_currentState)
                _currentState.Off();
            _currentState = newState;
            _currentState.On();
        }
    }
}