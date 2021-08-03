using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Doors.Data
{
    public class ConectedDoorData : DataProperty
    {
        public Door ValueDoor => valueDoor;
        [SerializeField] private Door valueDoor;

        public ConectedDoorData(Door doorForCnected) => valueDoor = doorForCnected;
    }
}