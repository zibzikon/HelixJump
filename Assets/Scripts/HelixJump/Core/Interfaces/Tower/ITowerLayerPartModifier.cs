using HelixJump.Core.Towers.Layers.Parts;

namespace HelixJump.Core.Interfaces.Tower
{
    public interface ITowerLayerPartModifier
    {
        void OnTowerLayerHit(TowerLayerPartHitInfo hitInfo);
    }
}