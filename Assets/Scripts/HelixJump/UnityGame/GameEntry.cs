using HelixJump.UnityGame.Builders;
using UnityEngine;

namespace HelixJump.UnityGame
{
    public class GameEntry : MonoBehaviour
    {
        [SerializeField] private UnityGameBuilder unityGameBuilder;
        private async void Start()
        {
            var game = await unityGameBuilder.BuildAsync();
            game.Start();
        }
    }
}