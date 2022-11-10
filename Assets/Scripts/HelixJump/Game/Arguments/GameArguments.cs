using HelixJump.Game.Interfaces.Arguments;

namespace HelixJump.Game.Arguments
{
    public class GameArguments : IGameArguments
    {
        public ITowersArguments TowersArguments { get; }
        public IPlayerArguments PlayerArguments { get; }

        public GameArguments(ITowersArguments towersArguments, IPlayerArguments playerArguments)
        {
            TowersArguments = towersArguments;
            PlayerArguments = playerArguments;
        }
    }
}