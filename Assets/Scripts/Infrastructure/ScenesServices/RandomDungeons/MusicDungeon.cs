using System;
using System.Collections;
using HabObjects;
using Infrastructure.ScenesServices.Lobby.Signals;
using Plugins.HabObject.DIContainer;
using Plugins.HabObject.GlobalSystem;
using Services.Sound;
using Services.Sound.Sound2DLoops;
using UnityEngine;

namespace Infrastructure.ScenesServices.RandomDungeons
{
    public class MusicDungeon : MonoBehaviour
    {
        [SerializeField] private SoundLoop2DSO _music;

        [DI] private SoundSystem _soundSystem;
        [DI(GameStateMachines.States.RandomDungeon.IdChanelLevel)]
        private EventChanel _chanel;

        [DIC]
        private void Init() => _chanel.AddListen<PlayerSpawned>(OnPlayeSpawned);

        private Coroutine _moveMusic;

        private void OnPlayeSpawned(PlayerSpawned obj)
        {
            _chanel.RemoveListen<PlayerSpawned>(OnPlayeSpawned);
            _soundSystem.Play(_music, SoundSystem.LoopAction.Start, out var transformSound);
            _moveMusic = StartCoroutine(MoveSound(obj.Actor, transformSound));
        }

        private void OnDestroy()
        {
            StopCoroutine(_moveMusic);
            _soundSystem.Play(_music, SoundSystem.LoopAction.Stop, out var transform);
        }

        private IEnumerator MoveSound(Actor player, Transform soundTransform)
        {
            while (true)
            {
                yield return null;
                soundTransform.position = player.transform.position;
            }
        }
    }
}