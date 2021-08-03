namespace HabObjects.Actors.Signals
{
    public class HealthUpdated
    {
        public float Current { get; }
        public float Max { get; }

        public HealthUpdated(float current, float max)
        {
            Current = current;
            Max = max;
        }
    }
}