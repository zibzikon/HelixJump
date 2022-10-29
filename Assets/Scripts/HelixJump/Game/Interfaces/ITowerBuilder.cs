using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Game.Interfaces
{
    public interface ITowerBuilder
    {
        ITower Build(int difficulty);
    }
}