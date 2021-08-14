using System;
using System.Collections.Generic;
using System.Linq;
using Factorys;
using HabObjects;
using HabObjects.Dungeons.Component;
using Infrastructure.GameStateMachines.States;
using Plugins.HabObject.DIContainer;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private DataEnemy.Difficulty _difficultyMonster;
        [SerializeField] private OneLoopAnimation _templateAnimation;
        [SerializeField] private Room _room;

        [DI] private EnemyFactory _enemyFactory;
        [DI] private DataDungeon _dataDungeon;
        
        private List<DataEnemy> _avaibelMonster;

        [DIC]
        private void Init() => _avaibelMonster = _dataDungeon.EnemyOnLevel.Where(x => x.DifficultyEnemy == _difficultyMonster).ToList();

        public void SpawnOrNull(Action<Actor> onSucses)
        {
            if (_avaibelMonster == null || _avaibelMonster.Count <= 0)
                return;
            
            var animation= Instantiate(_templateAnimation, transform.position, quaternion.identity);
            animation.EndAnimation += () =>
            {
                var enemy = _enemyFactory.Create(_avaibelMonster[Random.Range(0, _avaibelMonster.Count)], transform.position, _room);
                onSucses?.Invoke(enemy);
            };
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.4f);
            Gizmos.color = Color.red;
            Vector3 position = transform.position;
            position.z = -1;
            Gizmos.DrawLine(position+Vector3.down, position+Vector3.up);
            Gizmos.DrawLine(position+Vector3.right, position+Vector3.left);
        }
    }
}