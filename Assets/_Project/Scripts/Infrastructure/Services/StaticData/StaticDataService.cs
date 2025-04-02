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
        private const string ConfigsPath = "Configs";
        
        private Dictionary<Type, Object> _staticData = new();

        public void LoadStaticData()
        {
            _staticData = Resources.LoadAll(ConfigsPath)
                .ToDictionary(key => key.GetType(), config => config);
        }

        public T GetData<T>() where T : class
        {
            if (_staticData.TryGetValue(typeof(T), out Object value))
                return value as T;

            throw new NullReferenceException($"Static data: {typeof(T)} not found");
        }
    }
}