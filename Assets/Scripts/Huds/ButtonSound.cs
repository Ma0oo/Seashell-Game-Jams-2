using System;
using Plugins.HabObject.DIContainer;
using Services.Sound;
using Services.Sound.Sound2Ds;
using UnityEngine;
using UnityEngine.UI;

namespace Huds
{
    [RequireComponent(typeof(Button))]
    public class ButtonSound : MonoBehaviour
    {
        [SerializeField] private Sound2DSO _soundEvent;
        
        private Button _button;
        private Camera _camera;

        [DI()] private SoundSystem _soundSystem;

        private void Awake()
        {
            _camera = Camera.main;
            _button = GetComponent<Button>();
        }

        private void OnEnable() => _button.onClick.AddListener(OnClick);

        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            CheckCamera();
            _soundSystem.Play(_soundEvent).transform.position = _camera.transform.position;
        }

        private void CheckCamera()
        {
            if (!_camera) _camera = Camera.main;
        }
    }
}