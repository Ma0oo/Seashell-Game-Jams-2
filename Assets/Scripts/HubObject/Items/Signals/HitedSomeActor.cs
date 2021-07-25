namespace HubObject.Items.Signals
{
    public class HitedSomeActor
    {
        public Actor HitedActor { get; }

        public HitedSomeActor(Actor hitedActor) => HitedActor = hitedActor;
    }
}