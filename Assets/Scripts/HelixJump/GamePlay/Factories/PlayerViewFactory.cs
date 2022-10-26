using HelixJump.Core.Interfaces;
using HelixJump.GamePlay.Views;
using UnityEngine;
using UnityEngine.Serialization;

namespace HelixJump.GamePlay.Factories
{
    [CreateAssetMenu(fileName = "PlayerViewFactory", menuName = "Factories/Player/PlayerViewFactory", order = 0)]
    public class PlayerViewFactory: GameObjectFactory
    {
        [FormerlySerializedAs("_playerVIewPrefab")] [SerializeField] private PlayerView playerViewPrefab;
        
        public PlayerView Get(IPlayer player,float towerLayersPadding, Vector3 towerPosition)
        {
            var playerViewInstance = CreateGameObject(playerViewPrefab);
            playerViewInstance.Initialize(player, towerLayersPadding, towerPosition);
            return playerViewInstance;
        }
    }
}