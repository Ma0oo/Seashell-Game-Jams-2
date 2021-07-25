namespace HubObject.Actors.Component.Interfaces
{
    public interface IInventory
    {
        public bool TryAdd(Item item);
        public bool TryRemove(Item item);
    }
}