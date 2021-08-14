using HabObjects.Dungeons.Component;
using HabObjects.Rooms.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace HabObjects.Rooms
{
    public class RoomSenderSignal : MonoBehaviour
    {
        [SerializeField] private Room _room;
        
        public void FireRoomCleaned() => _room.BloodSystem.Fire(new RoomCleaned());
    }
}