using HelixJump.Core.Interfaces;
using HelixJump.GameVIew.Views;
using UnityEngine;

namespace HelixJump.GameVIew.Builders
{
    [CreateAssetMenu(fileName = "PlayerViewBuilder", menuName = "Builders/Player/PlayerViewBuilder", order = 0)]
    public class PlayerViewBuilder: GameObjectBuilder
    { 
        [SerializeField] private PlayerView playerViewPrefab;
        
        public PlayerView Get(IPlayer player,float towerLayersPadding, Vector3 towerPosition)
        {
            var playerViewInstance = CreateGameObject(playerViewPrefab);
            playerViewInstance.Initialize(player, towerLayersPadding, towerPosition);
            return playerViewInstance;
        }
    }
}