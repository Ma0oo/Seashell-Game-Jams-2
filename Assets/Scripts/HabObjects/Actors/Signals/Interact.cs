namespace HabObjects.Actors.Signals
{
    public class Interact
    {
        public Actor Sender { get; }
        public Interact(Actor sender) => Sender = sender;
    }
}