using System.Collections;
using HubObject.Actors.Data;
using Plugins.HubObject.GlobalSystem;
using Services.Interfaces;
using UnityEngine;

namespace HubObject.Actors.Component.Player
{
    public class SpeedPlayer : MonoBehaviour
    {
        public float Value => _currentSpeed;
        
        [Min(0.1f)] [SerializeField] private float _acseleration;
        [SerializeField] private Actor _actor;

        private MaxSpeedData _maxSpeed;
        private float _currentSpeed;
        private IInput _input;
        private Coroutine _acionChangeValue;

        public void Awake() => _input = ServicesLocator.MainContainer.ResolveSingle<IInput>();

        private void Start() => _maxSpeed = _actor.GeneralContainer.GetOrNull<MaxSpeedData>();

        private void OnEnable() => _input.ChangeMove += OnChangeMove;

        private void OnDisable() => _input.ChangeMove -= OnChangeMove;

        private void OnChangeMove(bool isMove)
        {
            StopChangeSpeed();
            if (isMove) _acionChangeValue = StartCoroutine(ChangeSpeed(_maxSpeed.Value));
            else _acionChangeValue = StartCoroutine(ChangeSpeed(0));
        }

        private void StopChangeSpeed()
        {
            if (_acionChangeValue != null)
                StopCoroutine(_acionChangeValue);
        }

        private IEnumerator ChangeSpeed(float target)
        {
            while (_currentSpeed != target)
            {
                _currentSpeed = Mathf.MoveTowards(_currentSpeed, target, Time.deltaTime * _acseleration);
                yield return null;
            }
        }
    }
}