using System;
using UnityEngine;

namespace Mechanics
{
    public class DisabelOnStart : MonoBehaviour
    {
        private void Start() => gameObject.SetActive(false);
    }
}