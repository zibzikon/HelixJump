using System.Threading.Tasks;

namespace HelixJump.GamePlay.Interfaces
{
    public interface IGameBuilder
    {
        Task<IGame> BuildAsync();
    }
}