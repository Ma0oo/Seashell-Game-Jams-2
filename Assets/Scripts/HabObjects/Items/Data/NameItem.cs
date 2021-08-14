using Plugins.HabObject.Customizable.Attributs;
using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Items.Data
{
    [CustomizableComponent("Компонент-дата, имя предмета", 0)]
    public class NameItem : DataProperty
    {
        public string Value => _name;
        [TString("Имя предмета", 25)]
        [SerializeField] private string _name;
    }
}