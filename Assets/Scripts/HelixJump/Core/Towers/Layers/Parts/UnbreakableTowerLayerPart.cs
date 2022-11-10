using System.Collections.Generic;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;

namespace HelixJump.Core.Towers.Layers.Parts
{
    public class UnbreakableTowerLayerPart : AbstractTowerLayerPart, IHittable
    {
        public override string Type => "unbreakable";
        public UnbreakableTowerLayerPart(IEnumerable<ITowerLayerPartModifier> towerLayerPartModifiers = default) : base(towerLayerPartModifiers)
        {
        }


        protected override void Hit(IHittable hittable)
        {
            hittable.Hit(new HitInfo(hittable));
        }

    }
}