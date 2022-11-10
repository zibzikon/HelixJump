using System.Collections.Generic;
using HelixJump.Game.Arguments;
using HelixJump.Game.Factories;

namespace HelixJump.Game.Interfaces.Arguments
{
    public interface ITowersArguments
    {
        TowerFactory TowerFactory { get; }
        IEnumerable<TowerArguments> TowerArgumentsCollection { get; }
    }
}