using HelixJump.Core;
using HelixJump.Core.Utils;

namespace HelixJump.Game.Extensions
{
    public static class ResolutionExtensions
    {
        public static Rotation GetPartRotation(this Resolution resolution)
        {
            const float oneTurn = 360;
            return _ = new Rotation(oneTurn / resolution.Value);
        }
    }
}