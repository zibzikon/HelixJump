using System;
using HelixJump.GamePlay.Interfaces;
using UnityEngine;

namespace HelixJump.GamePlay
{
    public class GameEntry : MonoBehaviour
    {
        private async void Start()
        {
            IGameBuilder gameBuilder = new HelixJumpGameBuilder();
            var game = await gameBuilder.BuildAsync();

            await game.LoadAsync();
            game.Run();
            
        }
    }
}