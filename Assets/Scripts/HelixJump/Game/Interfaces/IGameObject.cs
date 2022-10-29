using System.Threading.Tasks;

namespace HelixJump.Game.Interfaces
{
    public interface IGameObject 
    {
        Task<IGameObject> ResetAndDisabledTask { get; }

        void ResetAndDisable();
        IGameObject Instantiate();
    }
}