using System;
using HabObjects.Actors.Component.Seller.Signal;
using HabObjects.Actors.Signals;
using HabObjects.Items.Data;
using Mechanics.Interfaces;
using Plugins.HabObject;
using TMPro;
using UnityEngine;

namespace HabObjects.Actors.Component.Seller
{
    public class ViewItemInGameSpace : MonoBehaviour, IInteractbleComponent
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private TextMeshPro _label;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public HabObject HabObject => _actor;
        public bool IsActive => enabled;

        private Item _currentItem;

        private void OnEnable()
        {
            _actor.BloodSystem.Track<SelectToInteract>(OnSelect);
            _actor.BloodSystem.Track<ViewItem>(OnViewItem);
            ChangeActiveViewElement(false);
        }

        private void OnDisable()
        {
            _actor.BloodSystem.Untrack<SelectToInteract>(OnSelect);
            _actor.BloodSystem.Untrack<ViewItem>(OnViewItem);
            ChangeActiveViewElement(false);
        }

        private void OnViewItem(ViewItem obj)
        {
            _currentItem = obj.Item;
            if(!_currentItem)
                return;
            _label.text = _currentItem.GeneralContainer.GetOrNull<NameItem>().Value;
            _spriteRenderer.sprite = _currentItem.GeneralContainer.GetOrNull<SpriteForInventory>().Value;
        }

        private void OnSelect(SelectToInteract obj) => ChangeActiveViewElement(obj.ToActive);

        private void ChangeActiveViewElement(bool toActive) => _label.enabled = toActive;
    }
}