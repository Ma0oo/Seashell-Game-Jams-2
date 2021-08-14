namespace HabObjects.Items.Signals
{
    public class Picked
    {
        public Actor HosterItme { get; }
        public Item PickedItem { get; }

        public Picked(Actor hostItme, Item pickedItem)
        {
            HosterItme = hostItme;
            PickedItem = pickedItem;
        }
    }
}