using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Items.Data
{
    public class SpriteForInventory : DataProperty
    {
        public Sprite Value => _sprite;
        [SerializeField] private Sprite _sprite;
    }
}