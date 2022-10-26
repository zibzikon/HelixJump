using HelixJump.Arguments;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Interfaces
{
    public interface ITowerLayerCreator 
    {
        string Type { get; }
        ITowerLayer Create(TowerLayerArguments towerLayerArguments);
    }
}