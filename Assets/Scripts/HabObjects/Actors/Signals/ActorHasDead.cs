namespace HabObjects.Actors.Signals
{
    public class ActorHasDead
    {
        public Actor _actorDead { get; }

        public ActorHasDead(Actor actorDead) => _actorDead = actorDead;
    }
}