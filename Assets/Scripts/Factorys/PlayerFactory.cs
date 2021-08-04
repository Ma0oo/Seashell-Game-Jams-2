using System.Collections.Generic;
using HabObjects;
using HabObjects.Actors.Component;
using Infrastructure.Data;
using Infrastructure.ScenesServices.Lobby;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Factorys
{
    public class PlayerFactory : MonoBehaviour
    {
        public Actor Actor { get; private set; }
        
        [DI] private DataPlayerProvider _dataProvider;
        [DI] private HudFactory _hudFactory;

        private DiServices _diServices = DiServices.MainContainer;
        
        public bool TryCreate(out Actor player, Vector3 positionSpawn)
        {
            player = null;
            DataPlayer playerData = null;
            
            if (!_dataProvider.GetData(out playerData))
                return false;

            SpawnPlayer(out player, playerData, positionSpawn);
            CreateItem(player, playerData);

            return true;
        }

        private void SpawnPlayer(out Actor player, DataPlayer playerData, Vector3 at)
        {
            Actor actor = ((GameObject) Resources.Load(playerData.PathPrefabPlayer, typeof(GameObject))).GetComponent<Actor>();
            player = _diServices.CreatePrefab(actor);
            Actor = player;
            player.transform.position = at;
        }

        private void CreateItem(Actor player, DataPlayer playerData)
        {
            List<Item> _items = new List<Item>();
            var inventory = player.ComponentShell.Get<Inventory>();
            foreach (var item in playerData.PathItemsPrefab)
            {
                Item newItem = SpawnItem(item);
                
                if (!inventory.TryAdd(newItem))
                {
                    Destroy(newItem.gameObject);
                    break;
                }
            }
        }

        private Item SpawnItem(string item)
        {
            Debug.Log(item);
            Item itemObject = ((GameObject)Resources.Load(item, typeof(GameObject))).GetComponent<Item>();
            return _diServices.CreatePrefab(itemObject);
        }
    }
}