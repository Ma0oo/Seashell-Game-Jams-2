using System;
using Plugins.HabObject.DIContainer;
using Services.Interfaces;
using UnityEngine;

namespace Mechanics
{
    public class ZoomCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [Min(0)] [SerializeField] private float _minZoom;
        [Min(0)] [SerializeField] private float _maxZoom;
        [Min(0.1f)] [SerializeField] private float _sensetivity; 
        
        [DI] private IInput _input;

        private void Update()
        {
            var orthographicSize = _camera.orthographicSize;
            orthographicSize += _input.DeltaScroll * _sensetivity;
            _camera.orthographicSize = Mathf.Clamp(orthographicSize, _minZoom, _maxZoom);
        }

        private void OnValidate()
        {
            if (_minZoom >= _maxZoom)
                _minZoom = _maxZoom;
        }
    }
}