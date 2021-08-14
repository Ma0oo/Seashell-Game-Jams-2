using HabObjects;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Factorys
{
    public class EnemyFactory
    {
        private DiServices _diContainer = DiServices.MainContainer;
        
        public Actor Create(DataEnemy data, Vector3 at, Room room = null)
        {
            var result = _diContainer.CreatePrefab(data.Template);
            result.transform.position = at;
            if(room)
                result.BloodSystem.Fire(new MonsterAddToRoom(room));
            return result;
        }
    }
}