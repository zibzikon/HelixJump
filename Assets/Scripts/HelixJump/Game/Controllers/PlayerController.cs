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
        
        
        public PlayerController(IPlayer player, IPlayerInput playerInput)
        {
            _player = player;
            _playerInput = playerInput;
        }
        
        public void Enable()
        {
            _playerInput.Enable();
            RegisterEvents();
        }

        public void Disable()
        {
            _playerInput.Disable();
            UnRegisterEvents();
        }

        private void RegisterEvents()
        {
            _playerInput.EnableHitMode += _player.EnableHitMode;
            _playerInput.DisableHitMode += _player.DisableHitMode;
        }
        
        private void UnRegisterEvents()
        {
            _playerInput.EnableHitMode -= _player.EnableHitMode;
            _playerInput.DisableHitMode -= _player.DisableHitMode;
        }
    }
}