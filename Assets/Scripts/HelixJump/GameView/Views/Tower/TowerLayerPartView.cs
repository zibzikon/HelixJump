using System.Threading.Tasks;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;
using UnityEngine;

namespace HelixJump.GameView.Views.Tower
{
    public class TowerLayerPartView : ViewBase
    {
        [SerializeField]
        private Animator animator;
        
        [SerializeField]
        private string destroyingAnimationParameterName;
        
        [SerializeField]
        private string towerType;
        public string TowerType => towerType;
        
        [SerializeField]
        private string type;
        public string Type => type;
        
        private ITowerLayerPart _towerLayerPartModel;
        private TaskCompletionSource<bool> _destroyingAnimationEndedTaskCompletionSource;

        public void Initialize(ITowerLayerPart towerLayerPartModel, Rotation rotation)
        {
            _towerLayerPartModel = towerLayerPartModel;
            transform.localRotation = Quaternion.Euler(0, rotation.Value, 0);
            _destroyingAnimationEndedTaskCompletionSource = new TaskCompletionSource<bool>();
           //OnTowerLayerPartModelDestroyedAsync();
           Enable();
        }
        
        private async void OnTowerLayerPartModelDestroyedAsync()
        {
            await _towerLayerPartModel.DestroyedTask;
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
            animator.SetBool(destroyingAnimationParameterName, true);
            await _destroyingAnimationEndedTaskCompletionSource.Task;
        }

        private void OnDestroyingAnimationEnded()
        {
            _destroyingAnimationEndedTaskCompletionSource.SetResult(true);
        }
        
    }
}