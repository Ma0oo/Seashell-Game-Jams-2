using Plugins.HabObject.Customizable.Attributs;
using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Items.Data
{
    [CustomizableComponent("Компонент-дата, описание предмета", 0)]
    public class ItemDescription : DataProperty
    {
        public string Value => _description;
        [TString("Описание предмета", 90)]
        [Multiline(15)][SerializeField] private string _description;
    }
}