using HabObjects.Actors.Data;

namespace Infrastructure.Data
{
    public class DataPlayer : IData
    {
        public string Name => "DataPlayer";

        public string PathPrefabPlayer;
        public int Force;
        public int Agility;
        public int Charisma;
        public int Magic;
        public int Money;
        public ClassActor.Class Class;
        public string[] PathItemsPrefab = new string[0];
        public float Health;
        public float HealthMax;
    }
}