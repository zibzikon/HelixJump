using System.Collections.Generic;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Core.Towers.Layers.Parts
{
    public class UnbreakableTowerLayerPart : AbstractTowerLayerPart, IHittable
    {
        public UnbreakableTowerLayerPart(IEnumerable<ITowerLayerPartModifier> towerLayerPartModifiers = default) : base(towerLayerPartModifiers)
        {
        }
        
        protected override void Hit(IHittable hittable)
        {
            hittable.Hit(new HitInfo(hittable));
        }

    }
}