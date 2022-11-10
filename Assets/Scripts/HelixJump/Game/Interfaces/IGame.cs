using System.Threading.Tasks;
using HelixJump.Core;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Game.Interfaces
{
    public interface IGame : IPause
    {
        int Difficulty { get; }
        ITower CurrentTower { get; }
        IPlayer CurrentPlayer { get; }
        Task<ITower> GameTowerChangedTask { get; }
        Task<IGame> GameLoseTask { get; }
        Task<IGame> GameWinTask { get; }
        void Start();
        void RestartLevel();
        void MoveNextLevel();
    }
}