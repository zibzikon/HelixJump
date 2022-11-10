using System.Threading.Tasks;
using HelixJump.Game.Interfaces;
using HelixJump.UnityGame.Interfaces.Arguments;

namespace HelixJump.GameView.UI.Interfaces
{
    public interface IGameUI
    {
        Task<bool> RestartGameButtonPressedTask { get; }
        Task<bool> MoveNextLevelButtonPressedTask { get; }
        Task<bool> PauseGameButtonPressedTask { get; }
        
        void Initialize(IGame game);
    }
}