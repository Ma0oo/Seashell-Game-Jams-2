namespace HabObjects.Actors.Signals
{
    public class DropThisItem
    {
        public Item Item { get; }

        public DropThisItem(Item item) => Item = item;
    }
}