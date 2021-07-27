using System;

namespace Hud.Inventorys
{
    public interface IInventoryView
    {
        public Type TypeInventoryForView { get; }
    }
}