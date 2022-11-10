using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;
using HelixJump.Game.Arguments;

namespace HelixJump.Game.Interfaces.Creators.Tower
{
    public interface ITowerLayerCreator 
    {
        ITowerLayer Create(TowerLayerArguments towerLayerArguments);
    }
}