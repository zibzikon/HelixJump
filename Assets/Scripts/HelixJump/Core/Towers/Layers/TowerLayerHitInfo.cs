using HelixJump.Core.Interfaces;

namespace HelixJump.Core.Towers.Layers
{
    public sealed class TowerLayerHitInfo : IHitInfo
    {
        public IHittable Hitter { get; }
        public int Position { get; }
        
        public TowerLayerHitInfo(int position, IHittable hitter)
        {
            Position = position;
            Hitter = hitter;
        }
    }
}