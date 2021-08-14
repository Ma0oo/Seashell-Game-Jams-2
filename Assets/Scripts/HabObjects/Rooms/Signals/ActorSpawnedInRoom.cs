namespace HabObjects.Rooms.Signals
{
    public class ActorSpawnedInRoom
    {
        public Actor Monster { get; }
        public Room SpawnedToThisRoom { get; }

        public ActorSpawnedInRoom(Actor monster, Room spawnedToThisRoom)
        {
            Monster = monster;
            SpawnedToThisRoom = spawnedToThisRoom;
        }
    }
}