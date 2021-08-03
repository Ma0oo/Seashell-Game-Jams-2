namespace HabObjects.Actors.Data
{
    public class MoneyUpdate
    {
        public int PastValue { get; }
        public int NewValue { get; }

        public MoneyUpdate(int pastValue, int newValue)
        {
            PastValue = pastValue;
            NewValue = newValue;
        }
    }
}