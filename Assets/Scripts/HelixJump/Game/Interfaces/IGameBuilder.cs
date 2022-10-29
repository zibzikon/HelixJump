using System.Threading.Tasks;

namespace HelixJump.Game.Interfaces
{
    public interface IGameBuilder
    {
        Task<IGame> BuildAsync();
    }
}