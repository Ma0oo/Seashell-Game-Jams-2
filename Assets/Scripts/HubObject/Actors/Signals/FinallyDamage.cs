namespace HubObject.Actors.Signals
{
    public class FinallyDamage
    {
        public float Damage { get; }

        public FinallyDamage(float damage) => Damage = damage;
    }
}