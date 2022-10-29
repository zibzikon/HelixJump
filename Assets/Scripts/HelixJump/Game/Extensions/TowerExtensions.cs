using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;

namespace HelixJump.Game.Extensions
{
    public static class TowerExtensions
    {
        public static Score CalculateTowerScore(this ITower tower)
        {
            var multiplier = 10;
            return new Score(tower.Capacity.Value * multiplier);
        }
    }
}