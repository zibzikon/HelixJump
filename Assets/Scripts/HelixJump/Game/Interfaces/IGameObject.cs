using System.Threading.Tasks;

namespace HelixJump.Game.Interfaces
{
    public interface IGameObject 
    {
        Task<IGameObject> ResetAndDisabledTask { get; }

        bool Disabled { get; }
        void ResetAndDisable();
        void Enable();
        IGameObject Instantiate();
    }
}