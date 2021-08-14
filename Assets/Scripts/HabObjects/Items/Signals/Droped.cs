namespace HabObjects.Items.Signals
{
    public class Droped
    {
        public Actor PrevHostItme { get; }
        public Item DropedItem { get; }

        public Droped(Actor hostItme, Item dropedItem)
        {
            PrevHostItme = hostItme;
            DropedItem = dropedItem;
        }
    }
}