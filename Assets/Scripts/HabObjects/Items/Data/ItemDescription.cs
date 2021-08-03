using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Items.Data
{
    public class ItemDescription : DataProperty
    {
        public string Value => _description;
        [Multiline(15)][SerializeField] private string _description;
    }
}