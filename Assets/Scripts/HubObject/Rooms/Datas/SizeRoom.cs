using System;
using Plugins.HubObject.Property;
using UnityEngine;

namespace HubObject.Rooms.Datas
{
    public class SizeRoom : DataProperty
    {
        public Transform CenterRoom => _centerRoom;
        public Vector2 SizeRoomAbsoluti => _sizeRoomAbsoluti;

        [SerializeField] private Transform _centerRoom;
        [SerializeField] private Vector2 _sizeRoomAbsoluti;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(_centerRoom.transform.position, new Vector3(_sizeRoomAbsoluti.x/2, _sizeRoomAbsoluti.y/2, 1));
        }
    }
}