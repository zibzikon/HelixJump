using System.Collections.Generic;

namespace HelixJump.Game.Arguments
{
    public class TowerLayerPartArguments
    {
        public string Type { get; set; }
        public List<TowerLayerPartModifierArguments> Modifiers { get; set; }
    }
}