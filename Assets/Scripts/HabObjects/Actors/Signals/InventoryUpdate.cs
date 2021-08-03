using System;
using System.Collections.Generic;

namespace HabObjects.Actors.Signals
{
    public class InventoryUpdate
    {
        public List<Item> Items { get; private set; }
        public Type TypeInventory { get; private set; }

        public InventoryUpdate(List<Item> items, Type typeInventory)
        {
            Items = items;
            TypeInventory = typeInventory;
        }
    }
}