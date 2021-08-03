using System;
using System.Collections;
using HabObjects;
using Infrastructure.ScenesServices.Lobby.Signals;
using Plugins.HabObject.DIContainer;
using Plugins.HabObject.GlobalSystem;
using Services.Sound;
using Services.Sound.Sound2DLoops;
using UnityEngine;

namespace Infrastructure.ScenesServices.Lobby
{
    public class MusicLobby : MonoBehaviour
    {
        [SerializeField] private SoundLoop2DSO _music;

        [DI] private SoundSystem _system;
        [DI(GameStateMachines.States.Lobby.ChanelLobbyId)] private EventChanel _eventChanel;

        private Coroutine _actionFollow;

        private void Awake()
        {
            _eventChanel.AddListen<LobbyFinishEnter>(OnFinishEnterLobby);
            _eventChanel.AddListen<LobbyStartExit>(OnExitLobby);
        }

        private void OnExitLobby(LobbyStartExit obj)
        {
            StopCoroutine(_actionFollow);
            _system.Play(_music, SoundSystem.LoopAction.Stop, out var tr);
        }

        private void OnFinishEnterLobby(LobbyFinishEnter obj)
        {
            _system.Play(_music, SoundSystem.LoopAction.Start, out var tr);
            _actionFollow = StartCoroutine(FollowSoundSource(tr, DiServices.MainContainer.ResolveSingle<Actor>(DIConstID.PlayerId).transform));
        }

        private IEnumerator FollowSoundSource(Transform sourceTransform, Transform target)
        {
            while (true)
            {
                sourceTransform.position = target.position;
                yield return null;
            }
        }
    }
}