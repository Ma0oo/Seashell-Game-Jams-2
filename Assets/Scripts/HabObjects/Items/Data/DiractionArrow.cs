using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Items.Data
{
    public class DiractionArrow : DataProperty
    {
        public Vector3 Diraction
        {
            get
            {
                Vector3 result = (_endPoint.position - _startPoint.position);
                result.z = 0;
                return result.normalized;
            }
        }

        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
    }
}