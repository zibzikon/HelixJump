using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Game.Interfaces.Creators.Tower
{
    public interface ITowerLayerPartModifiersCreator
    {
        ITowerLayerPartModifier Create();
        string Type { get; set; }
    }
}