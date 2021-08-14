using System;
using System.Collections;
using Factorys;
using HabObjects;
using HabObjects.Actors.Signals;
using Infrastructure;
using Infrastructure.Services;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Mechanics
{
    public class RemoverCurrentProfileByDead : MonoBehaviour
    {
        [DI] private PlayerFactory _playerFactory;
        [DI] private ICoroutineRunner _coroutineRunner;
        [DI] private ProfileProvider _profileProvider;

        private Actor _player;
        
        private void Awake() => _coroutineRunner.StartCoroutine(TrackInstancePlayer());

        private IEnumerator TrackInstancePlayer()
        {
            while (_playerFactory.CurrentActor==null)
                yield return null;
            _player = _playerFactory.CurrentActor;
            _player.BloodSystem.Track<ActorHasDead>(OnPlayerDead);
        }

        private void OnPlayerDead(ActorHasDead @event) => _profileProvider.Remove(_profileProvider.CurrentProfile);
    }
}