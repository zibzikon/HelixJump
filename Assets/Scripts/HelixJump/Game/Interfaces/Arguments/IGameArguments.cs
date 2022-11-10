namespace HelixJump.Game.Interfaces.Arguments
{
    public interface IGameArguments
    {
        ITowersArguments TowersArguments { get; }
        IPlayerArguments PlayerArguments { get; }
    }
}