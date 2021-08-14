using System;
using System.Security.Cryptography;
using HabObjects.Actors.Component.Seller.Signal;
using HabObjects.Actors.Data;
using HabObjects.Actors.Signals;
using Mechanics.Interfaces;
using Plugins.HabObject;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace HabObjects.Actors.Component.Seller
{
    public class Seller : MonoBehaviour, IInteractbleComponent
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private TextMeshPro _labelCost;
        [SerializeField] private LoaderItem _loaderItem;
        [SerializeField] private UnityEvent _onBuy;

        public HabObject HabObject => _actor;
        public bool IsActive => enabled;

        private Item _itemToSell;
        private int _cost = 0;

        private void Awake()
        {
            _actor.BloodSystem.Track<Interact>(OnInteract);
            _actor.BloodSystem.Track<SelectToInteract>(OnSelect);
            _labelCost.enabled = false;
        }

        private void Start()
        {
            _itemToSell = _loaderItem.LoadItemOrNull();
            if(!_itemToSell)
                return;
            _actor.BloodSystem.Fire(new ViewItem(_itemToSell));
            var costData = _itemToSell.GeneralContainer.GetOrNull<CostItem>();
            _labelCost.text = costData != null ? costData.Value + " $" : "Free";
            _cost = costData != null ? costData.Value : 0;
        }

        private void OnSelect(SelectToInteract obj) => _labelCost.enabled = obj.ToActive;

        private void OnInteract(Interact obj)
        {
            var money = obj.Sender.ComponentShell.Get<Money>();
            if (money.TryChangeAt(_cost * -1))
            {
                if (obj.Sender.ComponentShell.Get<Inventory>().TryAdd(_itemToSell))
                {
                    _onBuy?.Invoke();
                    Destroy(_actor.gameObject);
                }
                else
                {
                    money.TryChangeAt(_cost);
                }
            }
        }
    }
}