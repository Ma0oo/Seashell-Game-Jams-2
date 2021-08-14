using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Configs;
using Plugins.HabObject;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace HabObjects.Actors.Component.Enemy
{
    public class FactoryMoney
    {
        [DI] private ConfigPrefabs _configPrefabs;

        private List<MoneyCoin> _moneyCoins = new List<MoneyCoin>();
        
        [DIC]
        private void Init()
        {
            var moneys = Resources.LoadAll<Actor>(_configPrefabs.PathToMoneyPrefabFolder);
            foreach (var coin in moneys) _moneyCoins.Add(coin.ComponentShell.Get<MoneyCoin>());
            _moneyCoins = _moneyCoins.OrderBy(x => x.Value).ToList();
        }
        
        public void CreateAt(int awardValue, Vector3 position, Action<Actor> callback = null)
        {
            var objectToInstance = GetTemplatesToSpawn(awardValue);
            foreach (var obj in objectToInstance)
            {
                var objectMoney = DiServices.MainContainer.CreatePrefab(obj);
                objectMoney.transform.position = position;
                callback?.Invoke(objectMoney);
            }
        }

        private List<Actor> GetTemplatesToSpawn(int currentValue)
        {
            List<Actor> objectToInstance = new List<Actor>();
            while (currentValue > 0)
            {
                MoneyCoin coint = GatMaxValueCoinBy(currentValue);
                currentValue -= coint.Value;
                objectToInstance.Add((Actor)coint.HabObject);
            }
            return objectToInstance;
        }

        private MoneyCoin GatMaxValueCoinBy(int currentValue)
        {
            for (int i = 0; i < _moneyCoins.Count; i++)
            {
                if (_moneyCoins[i].Value > currentValue)
                    return i > 0 ? _moneyCoins[i - 1] : _moneyCoins[i];
                if (_moneyCoins[i].Value == currentValue)
                    return _moneyCoins[i];
            }
            return _moneyCoins[_moneyCoins.Count - 1];
        }
    }
}