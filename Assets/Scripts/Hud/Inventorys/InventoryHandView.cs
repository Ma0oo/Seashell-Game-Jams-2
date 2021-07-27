using System;
using HubObject;
using HubObject.Actors.Component;
using HubObject.Actors.Component.Interfaces;
using HubObject.Actors.Signals;
using UnityEngine;

namespace Hud.Inventorys
{
    public class InventoryHandView : MonoBehaviour, IInventoryView
    {
        public Type TypeInventoryForView => typeof(InventoryHand);
        
        [SerializeField] private Actor _actor;
        [SerializeField] private InventoryCell _cellTemplate;
        [SerializeField] private Transform _content;

        private InventoryCell _currentCell;
        
        private void Awake() => _actor.BloodSystem.Track<InventoryUpdate>(OnInventoryUpdate);

        private void OnInventoryUpdate(InventoryUpdate @event)
        {
            if (@event.TypeInventory == typeof(InventoryHand))
            {
                TryThrowException(@event);
                UnsubscribeToEventCell();
                if (@event.Items.Count > 0)
                {
                    if (!_currentCell)
                        CreateCell(@event);
                    else
                        InitCurrentCell(@event);
                }
                else
                {
                    DeleteCurrentCell();
                }
            }
        }

        private void InitCurrentCell(InventoryUpdate @event)
        {
            _currentCell.Init(@event.Items[0], transform);
        }

        private void CreateCell(InventoryUpdate @event)
        {
            _currentCell = Instantiate(_cellTemplate, _content);
            _currentCell.Init(@event.Items[0], transform);
            SubscribeToEventCell();
        }

        private void SubscribeToEventCell()
        {
            _currentCell.Droped += OnDrop;
            _currentCell.TransitTo += OnTransit;
        }
        
        private void UnsubscribeToEventCell()
        {
            if (_currentCell)
            {
                _currentCell.Droped -= OnDrop;
                _currentCell.TransitTo -= OnTransit;
            }
        }

        private void DeleteCurrentCell()
        {
            if(_currentCell)
                Destroy(_currentCell.gameObject);
        }

        private static void TryThrowException(InventoryUpdate @event)
        {
            if (@event.Items.Count > 1)
                throw new Exception("View hand and hand can't has more then 1 item");
        }
        
        private void OnDrop(Item item) => _actor.ComponentShell.Get<InventoryHand>().TryRemove(item);

        private void OnTransit(Type typeNewInventory, Item item)
        {
            OnDrop(item);
            (_actor.ComponentShell.Get(typeNewInventory) as IInventory).TryAdd(item);
        }

    }
}