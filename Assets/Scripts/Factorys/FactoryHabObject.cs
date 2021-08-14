using HabObjects;
using Plugins.HabObject;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Factorys
{
    public class FactoryHabObject
    {
        public HabObject Create(HabObject template, Vector3 at)
        {
            var result = DiServices.MainContainer.CreatePrefab(template);
            result.transform.position = at;
            return result;
        }
        
        public HabObject Create(HabObject template, Vector3 at, Room room)
        {
            var result = DiServices.MainContainer.CreatePrefab(template);
            result.transform.position = at;
            result.transform.SetParent(room.transform);
            return result;
        }
    }
}