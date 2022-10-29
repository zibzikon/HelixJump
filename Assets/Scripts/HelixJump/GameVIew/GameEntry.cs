using HelixJump.Game.Builders;
using HelixJump.Game.Interfaces;
using UnityEngine;

namespace HelixJump.GameVIew
{
    public class GameEntry : MonoBehaviour
    {
        private async void Start()
        {
            IGameBuilder gameBuilder = new HelixJumpGameBuilder();
            var game = await gameBuilder.BuildAsync();

            
            var gameController = new GameController();
            
            game.Run();
            
        }
    }
}