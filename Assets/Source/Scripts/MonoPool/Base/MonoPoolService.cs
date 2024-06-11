using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MonoPool
{
    public class MonoPoolService : IMonoPoolService
    {
        private MonoPoolServiceConfig _monoPoolServiceConfig;
        
        private IObjectResolver _objectResolver;
        private Dictionary<Type, HashSet<MonoPoolable>> _pool = new();

        [Inject]
        private void Construct(MonoPoolServiceConfig monoPoolServiceConfig, IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
            _monoPoolServiceConfig = monoPoolServiceConfig;
        }
        
        public MonoPoolable Get<T>(Vector3 position) where T : MonoPoolable
        {
            if (!Contains<T>()) Instantiate<T>();

            var monoPoolable = _pool[typeof(T)].SnatchFirst();

            monoPoolable.transform.position = position;
            
            monoPoolable.gameObject.SetActive(true);
            
            return monoPoolable;
        }

        public void Release<T>(MonoPoolable monoPoolable) where T : MonoPoolable
        {
            if (Contains(monoPoolable)) return;

            _pool[typeof(T)].Add(monoPoolable);
            
            monoPoolable.gameObject.SetActive(false);
            
            monoPoolable.ResetState();
        }

        private void Instantiate<T>()
        {
            var key = typeof(T);
            var prefab = _monoPoolServiceConfig.Poolables[key];

            var obj = _objectResolver.Instantiate(prefab, _monoPoolServiceConfig.SpawnPosition, Quaternion.identity);
            
            if (!_pool.ContainsKey(key)) _pool[key] = new();
                
            _pool[key].Add(obj);
            
            obj.ResetState();
        }

        private bool Contains<T>() where T : MonoPoolable
        {
            var key = typeof(T);
            
            return _pool.ContainsKey(key) && _pool[key].Any();
        }
        
        private bool Contains<T>(T monoPoolable) where T : MonoPoolable
        {
            var key = typeof(T);
            
            return _pool.ContainsKey(key) && _pool[key].Contains(monoPoolable);
        }
    }
}