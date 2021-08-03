﻿using HabObjects.Actors.Signals;
using Infrastructure;
using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States;
using Infrastructure.ScenesServices.Lobby;
using Mechanics;
using Mechanics.Interfaces;
using Mechanics.LevelConditionOpen;
using Plugins.HabObject.DIContainer;
using TMPro;
using UnityEngine;

namespace HabObjects.Actors.Component.DoorsAndOtherTransitBeh
{
    public class EnterToDungeon : MonoBehaviour, IInteractbleComponent
    {
        public HabObject HabObject => _parent;
        public bool IsActive => enabled;
        
        [SerializeField] private HabObject _parent;
        [SerializeField] private DataDungeon _dungeonData;
        [SerializeField] private TextMeshPro _label;
        [SerializeField] private ConditionOpenDungeon _conditionOpen;
        [SerializeField] private SwitchSpriteByEnable _switchSprite;
        
        [DI] private GameStateMachine _gameStateMachine;
        [DI] private Curtain _curtain;
        [DI] private DataPlayerProvider _playerProvider;

        private void Awake()
        {
            SetTextLabel();
            CheckCondition();
        }

        private void OnEnable()
        {
            StartTrackInterectSignal();
        }

        private void OnSelect(SelectToInteract obj) => _label.enabled = obj.ToActive;

        private void OnInteract(Interact obj)
        {
            enabled = false;
            _curtain.Transit(()=>
            {
                _playerProvider.SavePlayer(DiServices.MainContainer.ResolveSingle<Actor>(DIConstID.PlayerId));
                _gameStateMachine.Enter<RandomDungeon, DataDungeon>(_dungeonData);
            });
        }

        private void CheckCondition() => _switchSprite.SetSprite(enabled = _conditionOpen.IsOpen());

        private void StartTrackInterectSignal()
        {
            _parent.BloodSystem.Track<Interact>(OnInteract);
            _parent.BloodSystem.Track<SelectToInteract>(OnSelect);
        }

        private void SetTextLabel()
        {
            _label.text = _dungeonData.NameLevel;
            _label.enabled = false;
        }
    }
}