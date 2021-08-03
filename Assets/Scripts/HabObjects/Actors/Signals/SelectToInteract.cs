namespace HabObjects.Actors.Signals
{
    public class SelectToInteract
    {
        public bool ToActive { get; }

        public SelectToInteract(bool toActive) => ToActive = toActive;
    }
}