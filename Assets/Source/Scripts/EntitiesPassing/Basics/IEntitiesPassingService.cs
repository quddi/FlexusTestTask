using System;

namespace EntitiesPassing
{
    public interface IEntitiesPassingService
    {
        public event Action<string, object> OnEntitySet;
        public event Action<string, object> OnEntityRemoved;
        
        public void Set(string key, object entity);

        public object Get(string key);

        public void Remove(string key);
    }
}