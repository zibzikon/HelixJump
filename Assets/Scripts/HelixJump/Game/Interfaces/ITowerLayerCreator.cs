using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game.Arguments;

namespace HelixJump.Game.Interfaces
{
    public interface ITowerLayerCreator 
    {
        string Type { get; }
        ITowerLayer Create(TowerLayerArguments towerLayerArguments);
    }
}