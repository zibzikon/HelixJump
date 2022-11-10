using System.Threading;
using HelixJump.Core.Interfaces;
using HelixJump.Game.Interfaces;
using TaskExtensions = HelixJump.Game.Extensions.TaskExtensions;

namespace HelixJump.Game.Controllers
{
    public class PlayerController : IPlayerController
    {
        private readonly IPlayer _player;
        private readonly IPlayerInput _playerInput;
        
        private CancellationTokenSource _cancellationTokenSource = new();
        
        public PlayerController(IPlayer player, IPlayerInput playerInput)
        {
            _player = player;
            _playerInput = playerInput;
        }
        
        public void Enable()
        {
            _playerInput.Enable();
            SubscribeEvents();
        }

        public void Disable()
        {
            _playerInput.Disable();
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new();
        }

        private void SubscribeEvents() 
        {
            var cancellationToken = _cancellationTokenSource.Token;
            
            TaskExtensions.OnTaskEnded(() => _playerInput.EnableHitModeTask, _player.EnableHitMode, cancellationToken);
            TaskExtensions.OnTaskEnded(() => _playerInput.DisableHitModeTask, _player.DisableHitMode, cancellationToken);
        }
    }
}