using System.Collections.Generic;
using System.Threading.Tasks;
using HelixJump.Core.Enums.Tower;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Core.Towers.Layers.Parts
{
    public abstract class AbstractTowerLayerPart : ITowerLayerPart
    {
        private readonly IEnumerable<ITowerLayerPartModifier> _towerLayerPartModifiers;
        
        public abstract TowerLayerPartType Type { get; }
        
        public TaskCompletionSource<bool> BreakTaskCompletionSource { get; protected set; } = new();

        public TaskCompletionSource<bool> DestroyedTaskCompletionSource { get; protected set; }  = new (); 

        protected AbstractTowerLayerPart(IEnumerable<ITowerLayerPartModifier> towerLayerPartModifiers)
        {
            _towerLayerPartModifiers = towerLayerPartModifiers;
        }

        protected abstract void Hit(IHittable hitter);

        public void Hit(IHitInfo hitInfo)
        {
            var hitter = hitInfo.Hitter;
            
            if (_towerLayerPartModifiers is not null)
                foreach (var towerLayerPartModifier in _towerLayerPartModifiers)
                    towerLayerPartModifier.OnTowerLayerHit(new TowerLayerPartHitInfo(this, hitter));

            Hit(hitter);
        }

        public virtual void Destroy()
        {
            DestroyedTaskCompletionSource.SetResult(true);
        }
        
        public void Break()
        {
            BreakTaskCompletionSource.SetResult(true);
        }
       
    }
}