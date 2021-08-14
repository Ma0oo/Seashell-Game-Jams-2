using Plugins.HabObject.Customizable.Attributs;
using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Items.Data
{
    [CustomizableComponent("Компонент-дата, спрайт в инвентаре", 0)]
    public class SpriteForInventory : DataProperty
    {
        public Sprite Value => _sprite;
        [TObject("Картинка для инвентаря")]
        [SerializeField] private Sprite _sprite;
    }
}