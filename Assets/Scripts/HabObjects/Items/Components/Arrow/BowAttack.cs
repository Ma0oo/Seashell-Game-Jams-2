using System;
using HabObjects.Items.Signals;
using Plugins.HabObject.DIContainer;
using Services.Interfaces;
using Services.Sound;
using Services.Sound.Sound2Ds;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HabObjects.Items.Components
{
    public class BowAttack : MonoBehaviour
    {
        [SerializeField] private Item _parentItem;
        [SerializeField] private SearchArrow _searcherArrow;
        [SerializeField] private Sound2DSO _soundFire;
        [SerializeField] private Sound2DSO _soundReady;

        [DI] private IInput _input;
        [DI] private SoundSystem _soundSystem;

        private PointForArrow _pointArrow;
        private Item _arrow;

        private void Awake()
        {
            enabled = false;
            _pointArrow = _parentItem.GeneralContainer.GetOrNull<PointForArrow>();
            if (!_pointArrow)
                throw null;
            _parentItem.BloodSystem.Track<ItemInHand>(OnHandTaked);
            _parentItem.BloodSystem.Track<ItemRemoveFromHand>(OnRemoveFromHand);
        }

        private void OnEnable()
        {
            _input.MainAttackClick += OnAttackClick;
            _input.MainAttackUnclick += OnAttackUnclick;
        }

        private void OnDisable()
        {
            _input.MainAttackClick -= OnAttackClick;
            _input.MainAttackUnclick -= OnAttackUnclick;
        }

        private void OnAttackClick()
        {
            if(EventSystem.current.IsPointerOverGameObject())
                return;

            var arrow = _searcherArrow.GetFirstArrowOrNull();
            if (arrow)
            {
                _soundSystem.Play(_soundReady);
                _searcherArrow.TakeArrowFromInventory(arrow);
                arrow.BloodSystem.Fire(new ArrowReadyToFire(_pointArrow, _parentItem.transform));
                _arrow = arrow;
            }
        }

        private void OnAttackUnclick()
        {
            if (_arrow)
            {
                _soundSystem.Play(_soundFire);
                _arrow.BloodSystem.Fire(new FireArrow());
                _arrow = null;
            }
        }

        private void OnRemoveFromHand(ItemRemoveFromHand obj) => enabled = false;

        private void OnHandTaked(ItemInHand obj) => enabled = true;
    }
}