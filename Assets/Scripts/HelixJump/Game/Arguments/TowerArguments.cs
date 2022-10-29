using System.Collections.Generic;

namespace HelixJump.Game.Arguments
{
    public class TowerArguments
    {
        public string Difficulty { get; set; }
        public List<TowerLayerArguments> TowerLayers { get; set; }
    }
}