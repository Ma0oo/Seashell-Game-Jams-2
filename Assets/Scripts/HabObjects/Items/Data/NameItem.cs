using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Items.Data
{
    public class NameItem : DataProperty
    {
        public string Value => _name;
        [SerializeField] private string _name;
    }
}