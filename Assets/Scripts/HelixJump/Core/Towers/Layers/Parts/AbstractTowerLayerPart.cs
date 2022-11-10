using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Core.Towers.Layers.Parts
{
    public abstract class AbstractTowerLayerPart : ITowerLayerPart
    {
        
        public abstract string Type { get; }
        public Task<bool> BrokenTask => _brokenTaskCompletionSource.Task;
        public Task<bool> DestroyedTask => _destroyedTaskCompletionSource.Task;

        private readonly IEnumerable<ITowerLayerPartModifier> _towerLayerPartModifiers;
        private TaskCompletionSource<bool> _brokenTaskCompletionSource = new();
        private TaskCompletionSource<bool> _destroyedTaskCompletionSource = new (); 
        protected AbstractTowerLayerPart(IEnumerable<ITowerLayerPartModifier> towerLayerPartModifiers)
        {
            _towerLayerPartModifiers = towerLayerPartModifiers;
        }

        protected abstract void Hit(IHittable hitter);

        public void Hit(IHitInfo hitInfo)
        {
            var hitter = hitInfo.Hitter;
            
            /*if (_towerLayerPartModifiers is not null && _towerLayerPartModifiers.Any())
                foreach (var towerLayerPartModifier in _towerLayerPartModifiers)
                    towerLayerPartModifier.OnTowerLayerHit(new TowerLayerPartHitInfo(this, hitter));*/

            Hit(hitter);
        }

        public virtual void Destroy()
        {
            _destroyedTaskCompletionSource.TrySetResult(true);
        }
        
        public void Break()
        {
            if(_brokenTaskCompletionSource.TrySetResult(true))
                UnityEngine.Debug.Log("AAAAAAAAAA");
        }
       
    }
}