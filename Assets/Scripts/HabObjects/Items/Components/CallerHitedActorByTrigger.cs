using System;
using System.Collections;
using System.Collections.Generic;
using HabObjects.Items.Signals;
using PhysicShell;
using UnityEngine;

namespace HabObjects.Items.Components
{
    public class CallerHitedActorByTrigger : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private TriggerShell _triggetHit;

        private List<Actor> _lastHitedActor = new List<Actor>();
        private Actor _hosterItem;

        private void Awake()
        {
            _item.BloodSystem.Track<Picked>(OnPicked);
            _item.BloodSystem.Track<Droped>(OnDroped);
            _item.BloodSystem.Track<ItemInHand>(OnItemInHand);
            _item.BloodSystem.Track<ItemRemoveFromHand>(OnItemRemoveFromHand);
            _triggetHit.enabled = false;
        }

        private void OnItemInHand(ItemInHand obj) => _triggetHit.enabled = true;

        private void OnItemRemoveFromHand(ItemRemoveFromHand obj) => _triggetHit.enabled = false;

        private void OnEnable() => _triggetHit.Enter += OnEnter;

        private void OnDisable() => _triggetHit.Enter -= OnEnter;

        private void OnEnter(Collider2D obj)
        {
            if (obj.TryGetComponent<Actor>(out var result))
                if (result != _hosterItem && !_lastHitedActor.Contains(result))
                {
                    _item.BloodSystem.Fire(new HitedSomeActor(result));
                    StartCoroutine(BlockActorToHited(result, 0.08f));
                }
        }

        private IEnumerator BlockActorToHited(Actor target, float time)
        {
            _lastHitedActor.Add(target);
            yield return new WaitForSeconds(time);
            _lastHitedActor.Remove(target);
        }

        private void OnDroped(Droped @event)
        {
            if (_hosterItem == @event.PrevHostItme) _hosterItem = null;
            else Debug.LogError("Предмет не может быть дропнут не хозяином");
        }

        private void OnPicked(Picked @event) => _hosterItem = @event.HosterItme;
    }
}