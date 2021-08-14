using System;
using System.Security.Cryptography;
using HabObjects.Actors.Component.Enemy.Goblin;
using HabObjects.Items.Components;
using HabObjects.Items.Signals;
using UnityEngine;

namespace HabObjects.Actors.Component.Enemy.Bomb
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private Timer _timer;
        [SerializeField] private EplosionHitedActor _eplosionHitedActor;

        private void Awake() => _actor.BloodSystem.Track<StartBomb>(OnStartBomb);

        private void OnStartBomb(StartBomb obj)
        {
            _actor.BloodSystem.Untrack<StartBomb>(OnStartBomb);
            _timer.Start();
            _actor.BloodSystem.Track<TimerFinish>(OnFinishTimer);
        }

        private void OnFinishTimer(TimerFinish obj)
        {
            foreach (var actor in _eplosionHitedActor.GetExplosedHitedActor())
            {
                _actor.BloodSystem.Fire(new HitedSomeActor(actor));
            }
            _actor.BloodSystem.Fire(new BombHasExplored());
            Destroy(_actor.gameObject);
        }
    }
}