using System;
using Object = UnityEngine.Object;

namespace HelixJump.Game.Interfaces
{
    public interface IGameObjectCreator
    {
        public T Create<T>(T prefab) where T : IGameObject;

    }
}