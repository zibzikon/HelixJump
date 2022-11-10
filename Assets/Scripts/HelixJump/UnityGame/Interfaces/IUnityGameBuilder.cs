using System.Threading.Tasks;

namespace HelixJump.UnityGame.Interfaces
{
    public interface IUnityGameBuilder
    {
        Task<IUnityGame> BuildAsync();
    }
}