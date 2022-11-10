using System.Collections.Generic;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Core.Towers.Layers.Parts
{
    public class WeaklyTowerLayerPart : AbstractTowerLayerPart
    {
        public override string Type => "weakly";

        public WeaklyTowerLayerPart(IEnumerable<ITowerLayerPartModifier> towerLayerPartModifiers = default) : base(towerLayerPartModifiers)
        {
        }
        
        protected override void Hit(IHittable hittable)
        {
            Break();
        }

    }
}