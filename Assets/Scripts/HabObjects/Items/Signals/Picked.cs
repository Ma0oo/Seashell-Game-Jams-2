namespace HabObjects.Items.Signals
{
    public class Picked
    {
        public Actor HosterItme { get; }

        public Picked(Actor hostItme) => HosterItme = hostItme;
    }
}