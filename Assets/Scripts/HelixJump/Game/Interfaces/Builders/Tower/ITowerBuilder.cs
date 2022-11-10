using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Game.Interfaces.Builders.Tower
{
    public interface ITowerBuilder
    {
        ITower Build(int difficulty);
    }
}