namespace HabObjects.Rooms.Signals
{
    public class DungeonMonsterSpawned
    {
        public Actor Monster { get; }
        
        public DungeonMonsterSpawned(Actor monster) => Monster = monster;
    }
}