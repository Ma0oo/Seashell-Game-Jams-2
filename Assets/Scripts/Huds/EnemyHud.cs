using HabObjects;
using Huds.Inventorys;
using UnityEngine;

namespace Huds
{
    public class EnemyHud : MonoBehaviour
    {
        [SerializeField] private Actor _target;

        private void Awake()
        {
            foreach (var initCom in GetComponentsInChildren<IActorInit>(true)) initCom.Init(_target);
        }
    }
}