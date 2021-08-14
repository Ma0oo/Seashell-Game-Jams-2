using System;
using System.Collections;
using Huds;
using UnityEngine;

namespace HabObjects.Actors.Component
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [Min(0)][SerializeField] private float _startValue;

        private float _currentValue;
        private Coroutine _timerAction;
        
        public void Start()
        {
            if(_timerAction!=null)
                return;

            _currentValue = _startValue;
            _timerAction = StartCoroutine(StepsTimer());
        }

        private IEnumerator StepsTimer()
        {
            while (_currentValue>0)
            {
                _currentValue -= Time.deltaTime;
                _actor.BloodSystem.Fire(new TimerUpdate(_currentValue, _startValue));
                yield return null;
            }
            _currentValue = 0;
            _actor.BloodSystem.Fire(new TimerUpdate(_currentValue, _startValue));
            _actor.BloodSystem.Fire(new TimerFinish());
        }
    }
}