using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game.Arguments;

namespace HelixJump.Game.Interfaces.Creators.Tower
{
    public interface ITowerLayerPartCreator
    {
        string Type { get; }
        ITowerLayerPart Create(TowerLayerPartArguments towerLayerPartArguments);
    }
}