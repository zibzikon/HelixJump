using UnityEngine;

namespace HelixJump.GameVIew.Builders
{
    public abstract class GameObjectBuilder : ScriptableObject
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