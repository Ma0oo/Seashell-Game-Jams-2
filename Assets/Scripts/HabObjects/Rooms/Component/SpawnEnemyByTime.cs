using System.Collections;
using HabObjects.Rooms.Signals;
using Mechanics;
using Plugins.HabObject.Customizable.Attributs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HabObjects.Rooms.Component
{
    [CustomizableComponent("Настройка спауна, спаун по одному врагу с задержкой",0)]
    public class SpawnEnemyByTime : MonoBehaviour
    {
        [SerializeField] private Room _room;
        
        [SerializeField] [Min(1)] [TRangeInt("Колличество врагов за цикл мин", 1, 100, new int[] {1, 4, 8})]
        private int _countEnemyOnCycleMin;
        
        [SerializeField] [Min(1)] [TRangeInt("Колличество врагов за цикл макс", 1, 100, new int[] {1, 4, 8})]
        private int _countEnemyOnCycleMax;
        
        [SerializeField] [Min(0.1f)] [TRangeFloat("Время между спауном", 0.2f, 10, new float[] {1f, 4, 8})]
        private float _delaySpawn;

        [SerializeField] private SpawnPoint[] _points;

        public void StartCycle() => StartCoroutine(Cycle(Random.Range(_countEnemyOnCycleMin, _countEnemyOnCycleMax)));

        private IEnumerator Cycle(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var point = RandomPoint;
                point.SpawnOrNull(x =>
                {
                    if (x)
                    {
                        _room.BloodSystem.Fire(new ActorSpawnedInRoom(x, _room));
                        x.transform.SetParent(point.transform);
                    }
                });
                yield return new WaitForSeconds(_delaySpawn);
            }
        }

        private SpawnPoint RandomPoint => _points[Random.Range(0, _points.Length)];

        private void OnValidate()
        {
            if (_countEnemyOnCycleMin >= _countEnemyOnCycleMax)
                _countEnemyOnCycleMin = _countEnemyOnCycleMax;
        }
    }
}