using Infrastructure.Data;
using Infrastructure.Data.Interface;
using Mechanics.Interfaces;
using Plugins.HabObject;
using UnityEngine;

namespace HabObjects.Actors.Data
{
    public class Money : MonoBehaviour, ISaveDataPlayer
    {
        [SerializeField] private HabObject _parent;
        [SerializeField] private int _value;

        public int Value=>_value;


        private void Awake() => _parent.BloodSystem.Track<ManualUpdateMoney>(e=>CallEventUpdate(0));

        public bool TryChangeAt(int count)
        {
            if (_value + count < 0)
                return false;

            _value += count;
            CallEventUpdate(count);
            return true;
        }

        private void CallEventUpdate(int count) => _parent.BloodSystem.Fire(new MoneyUpdate(_value - count, _value));

        public void Save(DataPlayer data) => data.Money = Value;

        public void Load(DataPlayer data) => _value = data.Money;
    }
}