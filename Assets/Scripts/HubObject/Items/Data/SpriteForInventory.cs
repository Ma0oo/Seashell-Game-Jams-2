using Plugins.HubObject.Property;
using UnityEngine;

namespace HubObject.Items.Data
{
    public class SpriteForInventory : DataProperty
    {
        public Sprite Value => _sprite;
        [SerializeField] private Sprite _sprite;
    }
}