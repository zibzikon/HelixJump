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

        public Task<IGame> GameLoseTask => _gameLoseTaskCompletionSource.Task;
        
        public Task<IGame> GameWinTask => _gameWinTaskCompletionSource.Task;
        
        private TaskCompletionSource<IGame> _gameLoseTaskCompletionSource= new();
        
        private TaskCompletionSource<IGame> _gameWinTaskCompletionSource= new();
        
        private TaskCompletionSource<ITower> _gameTowerChangedTaskCompletionSource = new();
        
        public Task<ITower> GameTowerChangedTask => _gameTowerChangedTaskCompletionSource.Task;

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
            SubscribeEvents();
            CurrentPlayer.StartMoving();
            
            _playerController.Enable();
        }

        private void SubscribeEvents()
        {
            var cancellationToken = CancellationToken.None;
            
            TaskExtensions.OnTaskEnded(()=> CurrentPlayer.PlayerHitTask, OnPlayerHit, cancellationToken);
            TaskExtensions.OnTaskEnded(()=> CurrentTower.LayerDestroyedTaskChangedTask, OnTowerDestroyed, cancellationToken);
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
            _gameLoseTaskCompletionSource.TrySetResult(this);
            _gameLoseTaskCompletionSource = new();
        }

        private void Win()
        {
            CurrentPlayer.AddScore(CurrentTower.CalculateTowerScore().Value);
            _gameWinTaskCompletionSource.TrySetResult(this);
            _gameWinTaskCompletionSource = new();
        }

        private void AddDifficulty()
        {
            Difficulty += 1;
        }
        
        private void ChangeLevel(int difficulty)
        {
            CurrentTower = _towerBuilder.Build(difficulty);
            CurrentPlayer.SetBaseTower(CurrentTower);
            
            _gameTowerChangedTaskCompletionSource.TrySetResult(CurrentTower);
            _gameTowerChangedTaskCompletionSource = new();
        }

        private void OnTowerDestroyed()
        {
            Win();
        }

        private void OnPlayerHit()
        {
            Lose();
        }
    }
}