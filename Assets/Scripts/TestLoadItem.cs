using System;
using HabObjects;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestLoadItem : MonoBehaviour
    {
        [SerializeField] private string _path;

        private void Awake()
        {
            Debug.Log(Resources.LoadAll<Item>(_path).Length);
        }
    }
}