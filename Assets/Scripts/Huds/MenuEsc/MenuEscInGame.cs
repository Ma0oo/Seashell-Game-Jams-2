using HabObjects;
using HabObjects.Actors.Component.Interfaces;
using HabObjects.Actors.Signals;
using Infrastructure;
using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States;
using Mechanics;
using Plugins.HabObject.DIContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Huds
{
    public class MenuEscInGame : MenuEsc
    {
        [SerializeField] private Button _suicideButton;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Color _offColorMenu;

        [DI] private KillerActor _killerActor;
        [DI(DIConstID.PlayerId)] private Actor _player;
        [DI] private GameStateMachine _gameStateMachine;
        [DI] private Curtain _curtain;

        private Color _onColorMenu;

        private void Awake()
        {
            _onColorMenu = _label.color;
            SubscribeButton();
            ChangeActiveMenuButton(false);
            _player.BloodSystem.Track<ActorHasDead>(e=> ChangeActiveMenuButton(true));
        }

        private void SubscribeButton()
        {
            _suicideButton.onClick.AddListener(() => _killerActor.InstanceKill(_player));
            _menuButton.onClick.AddListener(() =>
            {
                _curtain.Fade(()=> _gameStateMachine.Enter<MainMenu>());
                _suicideButton.interactable = false;
                ChangeActiveMenuButton(false);
            });
        }

        private void ChangeActiveMenuButton(bool active)
        {
            Debug.Log(_menuButton.interactable);
            _menuButton.interactable = active;
            Debug.Log(_menuButton.interactable);
            _label.color = active ? _onColorMenu : _offColorMenu;
        }

        private void OnEnable() => 
            ChangeActiveMenuButton(_player.ComponentShell.Get<HealthAbs>().Current == 0);
    }
}