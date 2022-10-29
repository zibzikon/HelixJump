using System.Collections.Generic;

namespace HelixJump.Game.Arguments
{
    public class TowerLayerArguments
    {
        public string Type { get; set; }
        public string Resolution { get; set; }
        public string Rotation { get; set; }
        public List<TowerLayerPartArguments> Parts { get; set; }
    }
}