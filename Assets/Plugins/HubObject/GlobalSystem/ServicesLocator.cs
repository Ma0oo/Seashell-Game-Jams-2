using System;
using System.Collections.Generic;

namespace Plugins.HubObject.GlobalSystem
{
    /// <summary>
    /// Simple Di container with Single and Transit object
    /// </summary>
    public sealed class ServicesLocator
    {
        /// <summary>
        /// Simple of DI 
        /// </summary>
        public static ServicesLocator MainContainer => _instance ?? (_instance = new ServicesLocator());

        private static ServicesLocator _instance;

        public delegate T TransitMethod<T>();

        private readonly Dictionary<Type, Dictionary<string, object>> _dictionaryTransit;
        private readonly Dictionary<Type, Dictionary<string, object>> _dictionarySingle;

        public ServicesLocator()
        {
            _dictionaryTransit = new Dictionary<Type, Dictionary<string, object>>();
            _dictionarySingle = new Dictionary<Type, Dictionary<string, object>>();
        }

        /// <summary>
        /// Requests a new instance by type 
        /// </summary>
        /// <param name="id">id of required type, default = ""</param>
        /// <typeparam name="T">The required type</typeparam>
        /// <returns>return new instance of T</returns>
        public T ResolveTransit<T>(string id = "") where T : class
        {
            if (!_dictionaryTransit.ContainsKey(typeof(T)))
                throw new Exception($"DI container does not contain this type  - Type: {typeof(T)}");

            if (!_dictionaryTransit[typeof(T)].ContainsKey(id))
                throw new Exception($"The container does not contain under this ID - Type: {typeof(T)} \\ Id: '{id}'");

            return ((TransitMethod<T>) _dictionaryTransit[typeof(T)][id]).Invoke();
        }

        /// <summary>
        /// Register a new instance by type 
        /// </summary>
        /// <param name="transitMethod">Method that will create a new type </param>
        /// <param name="id">id of required type, default = ""</param>
        /// <typeparam name="T">The required type</typeparam>
        public void RegisterTransit<T>(TransitMethod<T> transitMethod, string id = "") where T : class
        {
            if (transitMethod == null)
                throw new Exception($"Transit Method Create is null - type {typeof(T)}");

            if (_dictionaryTransit.ContainsKey(typeof(T)))
                if (_dictionaryTransit[typeof(T)].ContainsKey(id))
                    throw new Exception($"DI container already contains this type '{typeof(T)}' and this ID '{id}' ");

            if (_dictionaryTransit.ContainsKey(typeof(T)))
            {
                _dictionaryTransit[typeof(T)].Add(id, transitMethod);

            }
            else
            {
                _dictionaryTransit.Add(typeof(T), new Dictionary<string, object>());
                _dictionaryTransit[typeof(T)].Add(id, transitMethod);
            }
        }

        /// <summary>
        /// Requests a single by type 
        /// </summary>
        /// <param name="id">id of required type, default = ""</param>
        /// <typeparam name="T">The required type</typeparam>
        public T ResolveSingle<T>(string id = "") where T : class
        {
            if (!_dictionarySingle.ContainsKey(typeof(T)))
                throw new Exception($"DI container does not contain this type  - Type: {typeof(T)}");

            if (!_dictionarySingle[typeof(T)].ContainsKey(id))
                throw new Exception($"The container does not contain under this ID - Type: {typeof(T)} \\ Id: '{id}'");

            return _dictionarySingle[typeof(T)][id] as T;
        }

        /// <summary>
        /// Register a new single by type 
        /// </summary>
        /// <param name="instance">instance of single</param>
        /// <param name="id">id of required type, default = ""</param>
        /// <typeparam name="T">The required type</typeparam>
        public void RegisterSingle<T>(T instance, string id = "")
        {
            if (instance == null)
                throw new Exception($"Instance is null - type {typeof(T)}");

            if (_dictionarySingle.ContainsKey(typeof(T)))
                if (_dictionarySingle[typeof(T)].ContainsKey(id))
                    throw new Exception($"DI container already contains this type '{typeof(T)}' and this ID '{id}' ");

            if (_dictionarySingle.ContainsKey(typeof(T)))
            {
                _dictionarySingle[typeof(T)].Add(id, instance);
            }
            else
            {
                _dictionarySingle.Add(typeof(T), new Dictionary<string, object>());
                _dictionarySingle[typeof(T)].Add(id, instance);
            }
        }
    }
}