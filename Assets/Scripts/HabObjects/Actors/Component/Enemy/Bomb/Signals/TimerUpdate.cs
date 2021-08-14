namespace Huds
{
    public class TimerUpdate
    {
        public float Current { get; }
        public float StartValue { get; }

        public TimerUpdate(float current, float startValue)
        {
            Current = current;
            StartValue = startValue;
        }
    }
}