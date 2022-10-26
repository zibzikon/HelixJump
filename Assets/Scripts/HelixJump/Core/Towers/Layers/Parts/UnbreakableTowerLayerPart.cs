using System.Collections.Generic;
using HelixJump.Core.Enums.Tower;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;

namespace HelixJump.Core.Towers.Layers.Parts
{
    public class UnbreakableTowerLayerPart : AbstractTowerLayerPart, IHittable
    {
        public override TowerLayerPartType Type => TowerLayerPartType.Unbreakable;
        public UnbreakableTowerLayerPart(IEnumerable<ITowerLayerPartModifier> towerLayerPartModifiers = default) : base(towerLayerPartModifiers)
        {
        }


        protected override void Hit(IHittable hittable)
        {
            hittable.Hit(new HitInfo(hittable));
        }

    }
}