using HelixJump.Game.Interfaces;

namespace HelixJump.UnityGame.Utils
{
    public class UnityObjectsCreator : IGameObjectCreator
    {
        public T Create<T>(T prefab) where T : IGameObject
        {
            return (T)prefab.Instantiate();
        }
    }
}