using System.Threading;
using System.Threading.Tasks;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game.Extensions;
using HelixJump.Game.Interfaces;

namespace HelixJump.Game
{
    public class HelixJumpGame : IGame
    {
        public int Difficulty { get; private set; }
        
        private readonly IPlayer _player;
        private readonly IPlayerInput _playerInput;
        private readonly ITowerBuilder _towerBuilder;


        private TaskCompletionSource<ITower> _gameTowerChangedTaskCompletionSource = new();
        public Task<ITower> GameTowerChangedTask => _gameTowerChangedTaskCompletionSource.Task;
        public ITower CurrentTower { get; private set; }

        public HelixJumpGame(IPlayer player, IPlayerInput playerInput, ITowerBuilder towerBuilder)
        {
            _player = player;
            _playerInput = playerInput;
            _towerBuilder = towerBuilder;
        }
        
        public void Run()
        {
        
        }

        public void MoveNextLevel()
        {
            CurrentTower = _towerBuilder.Build(Difficulty);
            _player.ChangeBaseTower(CurrentTower);
            
            _gameTowerChangedTaskCompletionSource.TrySetResult(CurrentTower);
            _gameTowerChangedTaskCompletionSource = new();
        }
        
        public void Lose()
        {
            _player.RemoveScore(CurrentTower.CalculateTowerScore().Value);
        }

        public void Win()
        {
            _player.AddScore(CurrentTower.CalculateTowerScore().Value);
        }
    }
}