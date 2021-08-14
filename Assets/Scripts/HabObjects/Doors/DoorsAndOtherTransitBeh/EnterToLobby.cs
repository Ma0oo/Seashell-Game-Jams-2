using System;
using HabObjects.Actors.Signals;
using Infrastructure;
using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States;
using Infrastructure.ScenesServices.Lobby;
using Infrastructure.Services;
using Mechanics;
using Mechanics.Interfaces;
using Plugins.HabObject;
using Plugins.HabObject.DIContainer;
using TMPro;
using UnityEngine;

namespace HabObjects.Actors.Component.DoorsAndOtherTransitBeh
{
    public class EnterToLobby : MonoBehaviour, IInteractbleComponent
    {
        public HabObject HabObject => _actor;
        public bool IsActive => enabled;

        [SerializeField] private TextMeshPro _label;
        [SerializeField] private Actor _actor;

        [DI] private DataPlayerProvider _data;
        [DI] private Curtain _curtain;
        [DI] private GameStateMachine _gameStateMachine;

        private void Awake() => _label.enabled = false;

        private void OnEnable() => StartTrackInterectSignal();

        private void OnDisable() => StopTrackInterectSignal();

        private void StopTrackInterectSignal()
        {
            _actor.BloodSystem.Untrack<Interact>(OnInteract);
            _actor.BloodSystem.Untrack<SelectToInteract>(OnSelect);
        }

        private void StartTrackInterectSignal()
        {
            _actor.BloodSystem.Track<Interact>(OnInteract);
            _actor.BloodSystem.Track<SelectToInteract>(OnSelect);
        }

        private void OnSelect(SelectToInteract obj) => _label.enabled = obj.ToActive;

        private void OnInteract(Interact obj)
        {
            enabled = false;
            _data.SavePlayer(obj.Sender);
            _curtain.Fade(()=>_gameStateMachine.Enter<Lobby>());
        }
    }
}