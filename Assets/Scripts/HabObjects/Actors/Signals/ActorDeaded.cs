namespace HabObjects.Actors.Signals
{
    public class ActorDeaded
    {
        public Actor _actorDead { get; }

        public ActorDeaded(Actor actorDead) => _actorDead = actorDead;
    }
}