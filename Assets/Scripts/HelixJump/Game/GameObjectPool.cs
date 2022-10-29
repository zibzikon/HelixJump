using System;
using System.Collections.Generic;
using System.Linq;
using HelixJump.Game.Interfaces;

namespace HelixJump.Game
{
    public class GameObjectPool<T> : IGameObjectPool<T> where T : IGameObject
    {
        private readonly IGameObjectCreator _creator;
        private readonly List<T> _prefabs;
        private readonly List<T> _objects = new();

        public GameObjectPool(IGameObjectCreator creator, IEnumerable<T> prefabs)
        {
            _creator = creator;
            _prefabs = prefabs.ToList();
        }
        
         public GameObjectPool(IGameObjectCreator creator, IEnumerable<T> prefabs, IEnumerable<T> objects) : this( creator, prefabs)
         {
             _objects = objects.ToList();
         }
        
        public T Get(Func<T, bool> selectionPattern)
        {
            var @object = _objects.FirstOrDefault(selectionPattern);

            if (@object is not null)
            {
                _objects.Remove(@object);
                return @object;
            }

            var prefab = _prefabs.FirstOrDefault(selectionPattern);
            if (prefab is null)
                throw new InvalidOperationException($"No objects in GameObjectPool {GetType()} with this selection pattern {selectionPattern}");

            return _creator.Create(prefab);
        }

        public void Return(T obj)
        {
            if (obj.ResetAndDisabledTask.IsCompleted == false)
                obj.ResetAndDisable();
            
            _objects.Add(obj);
        }
    }
}