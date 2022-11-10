using System;
using HelixJump.Game.Interfaces;

namespace HelixJump.UnityGame.Interfaces
{
    public interface IGameObjectPool<T> where T : IGameObject
    {
        T Get(Func<T, bool> selectionPattern);
        void Return(T obj);
    }
}