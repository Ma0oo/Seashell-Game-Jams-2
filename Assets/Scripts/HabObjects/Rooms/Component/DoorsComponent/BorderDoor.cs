using System.Collections.Generic;
using HabObjects.Doors.Data;
using UnityEngine;

namespace HabObjects.Rooms.Component.DoorsComponent
{
    public class BorderDoor : MonoBehaviour
    {
        [SerializeField] private Door[] _doors;
        
        public List<Door> TryGetFreeDoors()
        {
            List<Door> result = new List<Door>();
            foreach (var door in _doors)
                if (!door.GeneralContainer.TryGet<ConectedDoorData>(out var data))
                    result.Add(door);

            return result;
        }

        public List<Door> TryGetOppositeDoors(Door otherDoor)
        {
            var dataDiraction = otherDoor.ComponentShell.Get<DiractionLookDoor>();
            List<Door> result = new List<Door>();
            foreach (var door in _doors)
                if (door.ComponentShell.Get<DiractionLookDoor>().IsOppositeByOtherDiraction(dataDiraction))
                    result.Add(door);
            return result;
        }
    }
}