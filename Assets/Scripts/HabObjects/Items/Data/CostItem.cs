using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Actors.Component.Seller
{
    public class CostItem : DataProperty
    {
        public int Value => _cost;
        [Min(0)][SerializeField] private int _cost;
    }
}