using System;
using System.Threading;
using System.Threading.Tasks;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game.Extensions;
using HelixJump.Game.Interfaces;
using HelixJump.Game.Interfaces.Builders.Tower;
using TaskExtensions = HelixJump.Game.Extensions.TaskExtensions;

namespace HelixJump.Game
{
    public class HelixJumpGame : IGame
    {
        public int Difficulty { get; private set; }

        public bool Paused { get; private set; }

        public ITower CurrentTower { get; private set; }

        public event Action GameLose;
        
        public event Action GameWin;

        public event Action LevelChanged;

        public IPlayer CurrentPlayer { get; }
        
        private readonly ITowerBuilder _towerBuilder;
        private readonly IPlayerController _playerController;

        public HelixJumpGame(IPlayer currentPlayer, IPlayerController playerController, ITowerBuilder towerBuilder, int startDifficulty = 1)
        {
            CurrentPlayer = currentPlayer;
            _playerController = playerController;
            _towerBuilder = towerBuilder;
            Difficulty = startDifficulty;
        }
        
        public void Start()
        {
            ChangeLevel(Difficulty);
            RegisterEvents();
            CurrentPlayer.StartMoving();
            
            _playerController.Enable();
        }

        private void RegisterEvents()
        {
            CurrentPlayer.Destroyed += OnPlayerDestroyed;
            CurrentTower.Destroyed += OnTowerDestroyed;
        }
        
        private void UnRegisterEvents()
        {
            CurrentPlayer.Destroyed -= OnPlayerDestroyed;
            CurrentTower.Destroyed -= OnTowerDestroyed;
        }
        
        public void RestartLevel()
        {
            ChangeLevel(Difficulty);
        }
        
        public void Pause()
        {
            Paused = false;
        }

        public void Resume()
        {
            Paused = true;
        }

        public void MoveNextLevel()
        {
            AddDifficulty();
            ChangeLevel(Difficulty);
        }

        private void Lose()
        {
            CurrentPlayer.RemoveScore(CurrentTower.CalculateTowerScore().Value);
           
        }

        private void Win()
        {
            CurrentPlayer.AddScore(CurrentTower.CalculateTowerScore().Value);
            
        }

        private void AddDifficulty()
        {
            Difficulty += 1;
        }
        
        private void ChangeLevel(int difficulty)
        {
            CurrentTower = _towerBuilder.Build(difficulty);
            CurrentPlayer.SetBaseTower(CurrentTower);
            
            LevelChanged?.Invoke();
        }

        private void OnTowerDestroyed(IDestroyable destroyable)
        {
            Win();
        }

        private void OnPlayerDestroyed(IDestroyable destroyable)
        {
            Lose();
        }
    }
}