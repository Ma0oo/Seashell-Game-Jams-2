using System.Collections.Generic;
using HabObjects.Actors.Component;
using HabObjects.Dungeons.Component;
using HabObjects.Items.Signals;
using Infrastructure;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace HabObjects.Rooms.Component
{
    public class TrackItemInRoom : MonoBehaviour
    {
        [SerializeField] private Room _room;

        [DI(DIConstID.PlayerId)] private Actor _player;

        private List<Item> _items = new List<Item>();

        private void Awake()
        {
            _room.BloodSystem.Track<StartRoom>(OnStartRoom);
            _room.BloodSystem.Track<StopRoom>(OnStopRoom);
        }

        private void OnStopRoom(StopRoom obj)
        {
            foreach (var item in GetPlayerItem())
            {
                item.BloodSystem.Untrack<Droped>(OnDropItem);
                item.BloodSystem.Untrack<Picked>(OnPickedItem);
            }
        }

        private void OnStartRoom(StartRoom obj)
        {
            foreach (var item in GetPlayerItem())
            {
                item.BloodSystem.Track<Droped>(OnDropItem);
                item.BloodSystem.Track<Picked>(OnPickedItem);
            }
        }

        private void OnDropItem(Droped obj) => _items.Add(obj.DropedItem);

        private void OnPickedItem(Picked obj) => _items.Remove(obj.PickedItem);

        private List<Item> GetPlayerItem()
        {
            var list = _player.ComponentShell.Get<Inventory>().GetItem(x => true);
            var hand = _player.ComponentShell.Get<InventoryHand>();
            if(hand.ItemInHand)
                list.Add(hand.ItemInHand);
            return list;
        }

        private void OnDestroy()
        {
            foreach (var item in _items)
                if(item)
                    if(item.gameObject)
                        Destroy(item.gameObject);
        }
    }
}