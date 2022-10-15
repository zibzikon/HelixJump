using HelixJump.Core.Interfaces;

namespace HelixJump.Core
{
    public class HitInfo : IHitInfo
    {
        public IHittable Hitter { get; }
        
        public HitInfo(IHittable hitter)
        {
            Hitter = hitter;
        }
    }
}