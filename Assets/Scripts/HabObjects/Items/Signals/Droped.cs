namespace HabObjects.Items.Signals
{
    public class Droped
    {
        public Actor PrevHostItme { get; }

        public Droped(Actor hostItme) => PrevHostItme = hostItme;
    }
}