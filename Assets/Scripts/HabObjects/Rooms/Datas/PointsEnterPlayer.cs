using System.Collections.Generic;
using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Dungeons.Component
{
    public class PointsEnterPlayer : DataProperty
    {
        public Transform RandomPoint => _point[Random.Range(0, _point.Count)];

        public List<Transform> Point
        {
            get
            {
                List<Transform> result = new List<Transform>();
                foreach (var p in _point) result.Add(p);
                return result;
            }
        }

        [SerializeField] private List<Transform> _point;
    }
}