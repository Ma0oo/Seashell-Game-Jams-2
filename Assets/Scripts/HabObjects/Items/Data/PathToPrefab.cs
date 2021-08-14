using Plugins.HabObject.Customizable.Attributs;
using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Items.Data
{
    [CustomizableComponent("Компонент-дата, путь к префабу", 0)]
    public class PathToPrefab : DataProperty
    {
        public string Path => _path;
        [TString("Путь к префабу в resources", 25)]
        [SerializeField] private string _path;
    }
}