using Plugins.HubObject.Property;
using UnityEngine;

namespace HubObject.Rooms.Component.DoorsComponent
{
    public class ConectedDoorData : DataProperty
    {
        public Door ValueDoor => valueDoor;
        [SerializeField] private Door valueDoor;

        public ConectedDoorData(Door doorForCnected) => valueDoor = doorForCnected;
    }
}