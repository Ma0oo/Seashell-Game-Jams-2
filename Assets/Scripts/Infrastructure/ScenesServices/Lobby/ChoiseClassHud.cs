using System;
using HabObjects.Actors.Data;
using Huds;
using Plugins.HabObject.DIContainer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Infrastructure.ScenesServices.Lobby
{
    public class ChoiseClassHud : Hud
    {
        public event Action Disabeling;
        public UnityEvent OnDIsabled;
        
        [SerializeField] private Button _buttonArch;
        [SerializeField] private Button _buttonKnight;
        [SerializeField] private Button _buttonMagic;

        [DI] private DataPlayerCreator _dataPlayerCreator;

        private Button[] _buttons => new[] {_buttonArch, _buttonKnight, _buttonMagic};

        [DIC]
        private void Construct()
        {
            _buttonArch.onClick.AddListener(()=>_dataPlayerCreator.Create(ClassActor.Class.Arch));
            _buttonKnight.onClick.AddListener(()=>_dataPlayerCreator.Create(ClassActor.Class.Knight));
            _buttonMagic.onClick.AddListener(()=>_dataPlayerCreator.Create(ClassActor.Class.Magic));
            foreach (var button in _buttons) button.onClick.AddListener(() => Disabeling?.Invoke());

            Disabeling += OnDisabeling;
        }

        private void OnDisabeling()
        {
            Disabeling -= OnDisabeling;
            foreach (var button in _buttons) button.interactable = false;
            OnDIsabled?.Invoke();
        }
    }
}