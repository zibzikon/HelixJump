using System.Threading.Tasks;

namespace HelixJump.Game.Interfaces
{
    public interface IPlayerInput
    {
        Task<bool> EnableHitModeTask { get; }
        Task<bool> DisableHitModeTask { get; }
        void Enable();
        void Disable();
    }
}