using System;

namespace HelixJump.Game.Interfaces
{
    public interface IGameObjectPool<T> where T : IGameObject
    {
        T Get(Func<T, bool> selectionPattern);
        void Return(T obj);
    }
}