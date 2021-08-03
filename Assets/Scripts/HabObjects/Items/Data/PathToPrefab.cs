using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Items.Data
{
    public class PathToPrefab : DataProperty
    {
        public string Path => _path;
        [SerializeField] private string _path;
    }
}