using System.Threading.Tasks;
using HelixJump.Game.Interfaces;
namespace HelixJump.UnityGame.Input
{
    public class PlayerInput : DefaultInput, IPlayerInput
    {
        public Task<bool> EnableHitModeTask => _enableHitModeTaskCompletionSource.Task;
        public Task<bool> DisableHitModeTask => _disableHitModeTaskCompletionSource.Task;

        private TaskCompletionSource<bool> _enableHitModeTaskCompletionSource = new();
        private TaskCompletionSource<bool> _disableHitModeTaskCompletionSource = new();

        protected override void OnLeftMouseButtonDown()
        {
            _enableHitModeTaskCompletionSource.TrySetResult(true);
            _enableHitModeTaskCompletionSource = new();
        }

        protected override void OnLeftMouseButtonUp()   
        {
            _disableHitModeTaskCompletionSource.TrySetResult(true);
            _disableHitModeTaskCompletionSource = new();
        }
        
    }
}