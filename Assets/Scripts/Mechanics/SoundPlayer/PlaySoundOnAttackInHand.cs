using System;
using HabObjects;
using HabObjects.Items.Signals;
using Plugins.HabObject.DIContainer;
using Services.Interfaces;
using Services.Sound;
using Services.Sound.Sound2Ds;
using UnityEngine;

namespace Mechanics
{
    public class PlaySoundOnAttackInHand : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private Sound2DSO _sound;

        [DI] private SoundSystem _soundSystem;
        [DI] private IInput _input;

        private void Awake()
        {
            _item.BloodSystem.Track<ItemInHand>(OnHand);
            _item.BloodSystem.Track<ItemRemoveFromHand>(OnRemove);
        }

        private void OnRemove(ItemRemoveFromHand obj) => _input.MainAttackClick -= AttackClick;

        private void OnHand(ItemInHand obj) => _input.MainAttackClick += AttackClick;

        private void AttackClick() => _soundSystem.Play(_sound);
    }
}