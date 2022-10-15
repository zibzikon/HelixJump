using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Core.Towers.Layers.Parts
{
    public struct TowerLayerPartHitInfo
    {
        public readonly IHittable Hitter;
        public readonly ITowerLayerPart TowerLayerPart;

        public TowerLayerPartHitInfo(ITowerLayerPart towerLayerPart, IHittable hitter)
        {
            TowerLayerPart = towerLayerPart;
            Hitter = hitter;
        }
    }
}