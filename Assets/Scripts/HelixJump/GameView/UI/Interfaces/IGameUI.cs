using System;
using System.Threading.Tasks;
using HelixJump.Game.Interfaces;
using HelixJump.UnityGame.Interfaces.Arguments;

namespace HelixJump.GameView.UI.Interfaces
{
    public interface IGameUI
    {
        event Action RestartGameButtonPressed;
        event Action MoveNextLevelButtonPressed;
        event Action PauseGameButtonPressed;

        void Initialize(IGame game);
    }
}