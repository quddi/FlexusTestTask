using System;
using System.Collections.Generic;

namespace EntitiesPassing
{
    public class EntitiesPassingService : IEntitiesPassingService
    {
        private Dictionary<string, object> _entities = new();
        
        public event Action<string, object> OnEntitySet;
        public event Action<string, object> OnEntityRemoved;
        
        public void Set(string key, object entity)
        {
            _entities[key] = entity;
            
            OnEntitySet?.Invoke(key, entity);
        }

        public object Get(string key)
        {
            return _entities.GetValueOrDefault(key);
        }

        public void Remove(string key)
        {
            if (!_entities.ContainsKey(key)) 
                return;

            var entity = _entities[key];

            _entities.Remove(key);
            
            OnEntityRemoved?.Invoke(key, entity);
        }
    }
}