using HabObjects;
using HabObjects.Items.Data;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Factorys
{
    public class FactoryItem
    {
        DiServices main = DiServices.MainContainer;

        public Item DublicateByPath(Item item)
        {
            Item prefab = Resources.Load<Item>(item.GeneralContainer.GetOrNull<PathToPrefab>().Path);
            return main.CreatePrefab(prefab);
        }

        public Item DublicateByInstance(Item Item) => main.CreatePrefab(Item);

        public Item SpawnByPath(string path)
        {
            Item prefab = Resources.Load<Item>(path);
            if (!prefab) throw null;
            return main.CreatePrefab(prefab);
        }
    }
}