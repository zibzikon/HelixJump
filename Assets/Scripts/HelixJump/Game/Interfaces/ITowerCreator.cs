using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game.Arguments;

namespace HelixJump.Game.Interfaces
{
    public interface ITowerCreator
    {
        ITower Create(TowerArguments towerArguments);
    }
}