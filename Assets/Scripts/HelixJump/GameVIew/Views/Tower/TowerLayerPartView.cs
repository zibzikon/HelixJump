using System.Threading.Tasks;
using HelixJump.Core.Enums.Tower;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;
using UnityEngine;

namespace HelixJump.GameVIew.Views.Tower
{
    public class TowerLayerPartView : ViewBase
    {
        [SerializeField]
        private Animator animator;
        
        [SerializeField]
        private string destroyingAnimationParameterName;
        
        [SerializeField]
        private TowerType towerType;
        public TowerType TowerType => towerType;
        
        [SerializeField]
        private TowerLayerPartType type;
        public TowerLayerPartType Type => type;
        
        private ITowerLayerPart _towerLayerPartModel;
        private TaskCompletionSource<bool> _destroyingAnimationEndedTaskCompletionSource;

        public void Initialize(ITowerLayerPart towerLayerPartModel, Rotation rotation)
        {
            _towerLayerPartModel = towerLayerPartModel;
            transform.localRotation = Quaternion.Euler(0, rotation.Value, 0);
            _destroyingAnimationEndedTaskCompletionSource = new TaskCompletionSource<bool>();
           OnTowerLayerPartModelDestroyedAsync();
           Initialize();
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