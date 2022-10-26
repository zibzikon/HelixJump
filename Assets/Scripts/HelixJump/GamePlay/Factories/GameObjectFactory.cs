using UnityEngine;

namespace HelixJump.GamePlay.Factories
{
    public abstract class GameObjectFactory : ScriptableObject
    {
        protected T CreateGameObject<T>(T prefab, Transform parent) where T : Object
        {
            return Instantiate(prefab, parent);
        }
        protected T CreateGameObject<T>(T prefab) where T : Object
        {
            return Instantiate(prefab);
        }
    }
}