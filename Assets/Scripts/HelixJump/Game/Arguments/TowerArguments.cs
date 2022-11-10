using System.Collections.Generic;

namespace HelixJump.Game.Arguments
{
    public class TowerArguments
    {
        public string Type { get; set; }
        public string Capacity { get; set; }
        public string Difficulty { get; set; }
        public string LayerRotationStep { get; set; }
        public List<TowerLayerArguments> TowerLayers { get; set; }
    }
}