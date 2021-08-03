using System;

namespace Huds.Inventorys
{
    public interface IInventoryView
    {
        public Type TypeInventoryForView { get; }
    }
}