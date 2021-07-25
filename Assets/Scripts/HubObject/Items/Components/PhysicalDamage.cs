using System;
using HubObject.Actors.Signals;
using HubObject.Items.Signals;
using JetBrains.Annotations;
using UnityEngine;

namespace HubObject.Items.Components
{
    public class PhysicalDamage : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private int _damageValue;

        private void Awake() => _item.BloodSystem.Track<HitedSomeActor>(OnHitedSomeActor);

        private void OnHitedSomeActor(HitedSomeActor @event) => @event.HitedActor.BloodSystem.Fire(new Damaged(_damageValue));
    }
}