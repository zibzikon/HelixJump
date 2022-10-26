using HelixJump.Core.Interfaces;

namespace HelixJump.Core.Utils
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