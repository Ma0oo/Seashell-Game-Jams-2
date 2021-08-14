using Infrastructure.GameStateMachines.States;
using Plugins.HabObject.DIContainer;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace HabObjects.Dungeons.Component
{
    public class SpawnerNewRoomDungeon : MonoBehaviour
    {
        [SerializeField] private Dungeon _dungeon;

        [DI] private NavMeshSurface2d _navMesh2D;
        
        public Room CurrentRoom { get; private set; }

        [DI] private DataDungeon _dataDungeon;

        public Room SpawnSafeRoom(Vector3 at) => CreateRoom(_dataDungeon.SafeRoom, at);

        public Room SpawnRoom(Vector3 at) => CreateRoom(_dataDungeon.PlayRooms[Random.Range(0, _dataDungeon.PlayRooms.Count)], at);

        private Room CreateRoom(Room template, Vector3 at)
        {
            var result = DiServices.MainContainer.CreatePrefab(template);
            result.transform.position = at;
            result.transform.rotation = quaternion.identity;
            CurrentRoom = result;
            _navMesh2D.BuildNavMesh();
            return result;
        }
    }
}