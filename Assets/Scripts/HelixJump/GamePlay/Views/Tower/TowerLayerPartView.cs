using System.Threading.Tasks;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;
using UnityEngine;

namespace HelixJump.GamePlay.Views.Tower
{
    public class TowerLayerPartView : ViewBase
    {
        [SerializeField]
        private Animator _animator;
        
        [SerializeField]
        private string _destroyingAnimationParameterName;
        
        private ITowerLayerPart _towerLayerPartModel;
        private TaskCompletionSource<bool> _destroyingAnimationEndedTaskCompletionSource;

        public void Initialize(ITowerLayerPart towerLayerPartModel, Rotation rotation)
        {
            _towerLayerPartModel = towerLayerPartModel;
            transform.localRotation = Quaternion.Euler(0, rotation.Value, 0);
            _destroyingAnimationEndedTaskCompletionSource = new TaskCompletionSource<bool>();
            OnTowerLayerPartModelDestroyed();
        }
        
        private async void OnTowerLayerPartModelDestroyed()
        {
            await _towerLayerPartModel.DestroyedTaskCompletionSource.Task;
            await PlayDestroyingAnimation();
            ResetAndDisable(); 
        }

        public override void ResetAndDisable()
        {
            _towerLayerPartModel = null;
            _destroyingAnimationEndedTaskCompletionSource = null;
            base.ResetAndDisable();
        }

        private async Task PlayDestroyingAnimation()
        {
            _animator.SetBool(_destroyingAnimationParameterName, true);
            await _destroyingAnimationEndedTaskCompletionSource.Task;
        }

        private void OnDestroyingAnimationEnded()
        {
            _destroyingAnimationEndedTaskCompletionSource.SetResult(true);
        }
        
    }
}