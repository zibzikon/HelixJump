using HelixJump.GameView.UI;
using HelixJump.GameView.UI.Interfaces;
using HelixJump.UnityGame.Interfaces.Arguments;
using UnityEngine;

namespace HelixJump.UnityGame.Arguments
{
    public class UnityGameUIArguments : MonoBehaviour , IUnityGameUIArguments
    {
        public IGameUI GameUI => gameUI;
        [SerializeField] private GameUI gameUI;
    }
}