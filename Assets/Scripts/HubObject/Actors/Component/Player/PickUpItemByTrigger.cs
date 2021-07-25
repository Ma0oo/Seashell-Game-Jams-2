using System;
using System.Collections.Generic;
using PhysicShell;
using Plugins.HubObject.GlobalSystem;
using Services.Interfaces;
using UnityEngine;

namespace HubObject.Actors.Component.Player
{
    public class PickUpItemByTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerShell _triggerPickUpItem;
        [SerializeField] private Actor _actor;

        private Inventory _inventory;
        private List<Item> _itemsForPickUp = new List<Item>();
        private IInput _input;

        private void Awake()
        {
            _inventory = _actor.ComponentShell.Get<Inventory>();
            _input = ServicesLocator.MainContainer.ResolveSingle<IInput>();
        }

        private void OnEnable()
        {
            _triggerPickUpItem.Enter += OnEnter;
            _triggerPickUpItem.Exit += OnExit;
            _input.Intractable += OnIntractable;
        }

        private void OnDisable()
        {
            _triggerPickUpItem.Enter -= OnEnter;
            _triggerPickUpItem.Exit -= OnExit;
            _input.Intractable -= OnIntractable;
        }

        private void OnIntractable()
        {
            if (_itemsForPickUp.Count > 0) _inventory.TryAdd(_itemsForPickUp[0]);
        }

        private void OnExit(Collider2D other)
        {
            if (other.TryGetComponent<Item>(out var result)) _itemsForPickUp.Remove(result);
        }

        private void OnEnter(Collider2D other)
        {
            if (other.TryGetComponent<Item>(out var result)) _itemsForPickUp.Add(result);
        }
    }
}