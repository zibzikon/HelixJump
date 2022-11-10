using System;
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
        public event Action<ITowerLayerPart> Broken;
        public event Action<IDestroyable> Destroyed ;

        private readonly IEnumerable<ITowerLayerPartModifier> _towerLayerPartModifiers;
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
            Destroyed?.Invoke(this);
        }
        
        public void Break()
        {
            Broken?.Invoke(this);
        }
       
    }
}