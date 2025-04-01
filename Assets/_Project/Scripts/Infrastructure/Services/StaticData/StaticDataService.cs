using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService :
        IStaticDataService
    {
        private Dictionary<Type, Object> _staticData = new();

        public void LoadStaticData()
        {
            _staticData = Resources.LoadAll("Configs").ToDictionary(n => n.GetType(), n => n);

            foreach (var data in _staticData)
                Debug.Log(data.Key.ToString());
        }

        public T GetData<T>() where T : class
        {
            if (_staticData.TryGetValue(typeof(T), out Object value))
                return value as T;

            throw new NullReferenceException($"Static data: {typeof(T)} not found");
        }
    }
}