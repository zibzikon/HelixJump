using System;
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
        event Action LevelChanged;
        event Action GameLose;
        event Action GameWin;
        void Start();
        void RestartLevel();
        void MoveNextLevel();
    }
}