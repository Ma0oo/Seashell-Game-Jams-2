using System;
using Factorys;
using Infrastructure.Configs;
using Plugins.HabObject.DIContainer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HabObjects.Actors.Component.Seller
{
    public class LoaderItem : MonoBehaviour
    {
        [SerializeField] private TypeItem _itemTarget;
        
        [DI] private FactoryItem _factoryItem;
        [DI] private ConfigPrefabs _config;

        public Item LoadItemOrNull()
        {
            var items = Resources.LoadAll<Item>(GetPath(_itemTarget));
            if (items.Length == 0)
                return null;

            var result = _factoryItem.DublicateByInstance(items[Random.Range(0, items.Length)]);
            result.gameObject.SetActive(false);
            return result;
        }

        private string GetPath(TypeItem typeItem)
        {
            switch (typeItem)
            {
                case TypeItem.All:
                    return _config.PathToAllItem;
                case TypeItem.botlle:
                    return _config.PathToBottleItem;
                case TypeItem.Weapon:
                    return _config.PathToWeaponItem;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeItem), typeItem, null);
            }
        }
        
        public enum TypeItem
        {
            All, botlle, Weapon
        }
    }
}