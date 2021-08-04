using System;
using HabObjects.Actors.Component;
using HabObjects.Items.Data;
using HabObjects.Items.Signals;
using UnityEngine;

namespace HabObjects.Items.Components
{
    public class SearchArrow : MonoBehaviour
    {
        [SerializeField] private Item _parentItem;

        private Inventory _invetoryToSearch;
        
        private void Awake()
        {
            _parentItem.BloodSystem.Track<Picked>(OnPicked);
            _parentItem.BloodSystem.Untrack<Droped>(OnDrop);
        }

        public Item GetFirstArrowOrNull()
        {
            if (!_invetoryToSearch)
                return null;

            var arrows = _invetoryToSearch.GetItem(x => x.GeneralContainer.GetOrNull<ArrowFlag>() != null);
            if (arrows.Count > 0)
                return arrows[0];
            return null;
        }

        public void DropItem(Item item)
        {
            if (_invetoryToSearch)
                _invetoryToSearch.TryRemove(item);
        }
        
        private void OnDrop(Droped obj) => _invetoryToSearch = null;

        private void OnPicked(Picked obj) => _invetoryToSearch = obj.HosterItme.ComponentShell.Get<Inventory>();
    }
}