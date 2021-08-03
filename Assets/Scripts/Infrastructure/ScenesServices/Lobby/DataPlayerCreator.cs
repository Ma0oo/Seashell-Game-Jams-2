using System;
using System.Collections.Generic;
using HabObjects;
using HabObjects.Actors.Data;
using Infrastructure.Data;
using Infrastructure.Data.Interface;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Infrastructure.ScenesServices.Lobby
{
    public class DataPlayerCreator : MonoBehaviour
    {
        [SerializeField] private List<ClassPrefab> _prefabs;

        public event Action DataCreated;

        [DI] private DataPlayerProvider _dataPlayerProvider;

        public void Create(ClassActor.Class classToSpawn)
        {
            ClassPrefab prefab = GetPrefab(classToSpawn);
            DataPlayer newData = new DataPlayer();
            foreach (var dataWithSave in prefab.PrefabActor.GetComponentsInChildren<ISaveDataPlayer>(true)) dataWithSave.Save(newData);
            //newData.PathPrefabPlayer = AssetDatabase.GetAssetPath(prefab.PrefabActor);
            newData.PathPrefabPlayer = prefab.PathToActor;

            newData.PathItemsPrefab = prefab.ItemsPath;
            _dataPlayerProvider.SaveData(newData);
            
            DataCreated?.Invoke();
        }

        private ClassPrefab GetPrefab(ClassActor.Class @class)
        {
            foreach (var cl in _prefabs)
            {
                if (cl.Class == @class)
                    return cl;
            }

            throw null;
        }

        [System.Serializable]
        private class ClassPrefab
        {
            public ClassActor.Class Class;
            public Actor PrefabActor;
            public string PathToActor;
            public string[] ItemsPath;
        }
    }
    
}