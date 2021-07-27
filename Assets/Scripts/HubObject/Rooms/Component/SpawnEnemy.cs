using System;
using HubObject.Actors.Data;
using HubObject.Rooms.Signals;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HubObject.Rooms.Component
{
    public class SpawnEnemy : MonoBehaviour
    {
        [SerializeField] private Room _room;
        [SerializeField] private Actor[] _actorsTemplates = new Actor[1];
        [SerializeField] private Transform[] _spawnPoint = new Transform[1];
        [Min(1)] [SerializeField] private int _countEnemy;

        private void OnEnable() => _room.BloodSystem.Track<StartSpawnEnemy>(OnStartSpawnEnemy);

        private void OnDisable() => _room.BloodSystem.Untrack<StartSpawnEnemy>(OnStartSpawnEnemy);

        private void Start() => _room.BloodSystem.Fire(new StartSpawnEnemy());

        private void OnStartSpawnEnemy(StartSpawnEnemy obj)
        {
            for (int i = 0; i < _countEnemy; i++)
            {
                Vector3 positionSpawn = _spawnPoint[Random.Range(0, _spawnPoint.Length)].position;
                Actor template = _actorsTemplates[Random.Range(0, _actorsTemplates.Length)];

                Actor newMonster = Instantiate(template, positionSpawn, quaternion.identity);
                _room.BloodSystem.Fire(new DungeonMonsterSpawned(newMonster));
            }
        }


        private void OnDrawGizmosSelected()
        {
            foreach (var point in _spawnPoint)
            {
                if (point != null)
                {
                    Gizmos.color = new Color(1f, 0.85f, 0.23f);
                    Gizmos.DrawWireSphere(point.position, 0.25f);
                    Gizmos.color = new Color(0.54f, 1f, 0.35f);
                    Gizmos.DrawWireSphere(point.position, 0.18f);
                }
            }
        }
    }
}