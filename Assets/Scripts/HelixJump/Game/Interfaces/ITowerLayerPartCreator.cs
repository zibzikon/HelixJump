using HelixJump.Arguments;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Interfaces
{
    public interface ITowerLayerPartCreator
    {
        string Type { get; }
        ITowerLayerPart Create(TowerLayerPartArguments towerLayerPartArguments);
    }
}