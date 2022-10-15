using HelixJump.Core.Towers.Layers.Parts;

namespace HelixJump.Core.Interfaces.Tower
{
    public interface ITowerLayerPartModifier
    {
        public void OnTowerLayerHit(TowerLayerPartHitInfo hitInfo);
    }
}