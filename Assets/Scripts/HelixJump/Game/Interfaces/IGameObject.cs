using System;
using System.Threading.Tasks;

namespace HelixJump.Game.Interfaces
{
    public interface IGameObject
    {
        event Action<IGameObject> ResetAndDisabled;
        bool Disabled { get; }
        void ResetAndDisable();
        void Enable();
        IGameObject Instantiate();
    }
}