using System.Collections;
using UnityEngine;

namespace HabObjects.Items.Components
{
    public class BrokeArrowOnHited : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [Range(0,1f)][SerializeField] private float _chance;

        private void Awake() => _item.BloodSystem.Track<ArrowHited>(OnHited);

        private void OnHited(ArrowHited obj)
        {
            if (Random.Range(0, 1f) < _chance)
            {
                _item.BloodSystem.Fire(new ArrowHasBroken());
                Destroy(_item.gameObject);
            }
        }
    }
}