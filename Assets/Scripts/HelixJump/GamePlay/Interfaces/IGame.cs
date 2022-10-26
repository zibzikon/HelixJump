using System.Threading;
using System.Threading.Tasks;

namespace HelixJump.GamePlay.Interfaces
{
    public interface IGame
    {
        Task LoadAsync(CancellationToken cancellationToken = default);
        void Run();
    }
}