using System;
using System.Collections.Generic;
using System.Linq;
using HelixJump.Game.Interfaces;
using HelixJump.UnityGame.Interfaces;

namespace HelixJump.UnityGame.Utils
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
             foreach (var poolObject in objects)
             {
                 poolObject.ResetAndDisable();
                 _objects.Add(poolObject);
             }
         }
        
        public T Get(Func<T, bool> selectionPattern)
        {
            var @object = _objects.FirstOrDefault(selectionPattern);

            if (@object is not null)
            {
                _objects.Remove(@object);
                @object.Enable();
                return @object;
            }

            var prefab = _prefabs.FirstOrDefault(selectionPattern);
            if (prefab is null)
                throw new InvalidOperationException($"No objects in GameObjectPool {GetType()} with this selection pattern {selectionPattern}");

            @object = _creator.Create(prefab);
            @object.Enable();
            return @object;
        }

        public void Return(T obj)
        {
            _objects.Add(obj);
        }
    }
}