using System.Threading.Tasks;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.UnityGame.Interfaces
{
    public interface IUnityGameLevelLoader
    {
        public Task LoadLevelAsync(ITower tower);
    }
}