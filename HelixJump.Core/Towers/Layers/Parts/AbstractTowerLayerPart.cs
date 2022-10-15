using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Core.Towers.Layers.Parts
{
    public abstract class AbstractTowerLayerPart : ITowerLayerPart
    {
        private readonly IEnumerable<ITowerLayerPartModifier> _towerLayerPartModifiers;
        
        public TaskCompletionSource<IDestroyable> DestroyedTaskCompletionSource { get; protected set; }  = new (); 

        protected AbstractTowerLayerPart(IEnumerable<ITowerLayerPartModifier> towerLayerPartModifiers)
        {
            _towerLayerPartModifiers = towerLayerPartModifiers;
        }

        protected abstract void Hit(IHittable hitter);

        public void Hit(IHitInfo hitInfo)
        {
            var hitter = hitInfo.Hitter;
            foreach (var towerLayerPartModifier in _towerLayerPartModifiers)
                towerLayerPartModifier.OnTowerLayerHit(new TowerLayerPartHitInfo(this, hitter));
            
            Hit(hitter);
        }

        public virtual void Destroy()
        {
            DestroyedTaskCompletionSource.SetResult(this);
        }
    }
}