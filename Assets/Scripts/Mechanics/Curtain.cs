using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Mechanics
{
    public class Curtain : MonoBehaviour
    {
        public event Action Faded;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AnimationCurve _curveFade;
        [SerializeField] private AnimationCurve _curveUnfade;
        [SerializeField] private float _duration;

        private float _currentDuration;
        private Coroutine _actionFade;
        
        

        private void Awake() => DontDestroyOnLoad(gameObject);

        public void Fade(Action onComplete)
        {
            StopCorutine();
            _actionFade = StartCoroutine(FadeTo(_curveFade, ()=>
            {
                Faded?.Invoke();
                onComplete?.Invoke();
            }));
        }

        public void Unfade()
        {
            StopCorutine();
            _actionFade = StartCoroutine(FadeTo(_curveUnfade));
        }

        public void Transit(Action onComplete)
        {
            Fade(() =>
            {
                onComplete?.Invoke();
                Unfade();
            });
        }

        private void StopCorutine()
        {
            if (_actionFade != null)
                StopCoroutine(_actionFade);
        }

        private IEnumerator FadeTo(AnimationCurve curve)
        {
            _currentDuration = 0;
            while (_currentDuration<=_duration)
            {
                _canvasGroup.alpha = curve.Evaluate(_currentDuration / _duration);
                _currentDuration += Time.deltaTime;
                yield return null;
            }
        }
        
        private IEnumerator FadeTo(AnimationCurve curve, Action callback)
        {
            _currentDuration = 0;
            while (_currentDuration<=_duration)
            {
                _canvasGroup.alpha = curve.Evaluate(_currentDuration / _duration);
                _currentDuration += Time.deltaTime;
                yield return null;
                
            }
            _canvasGroup.alpha = Mathf.Round(curve.Evaluate(2));
            callback?.Invoke();
        }
    }
}