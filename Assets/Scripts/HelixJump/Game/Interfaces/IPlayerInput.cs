
using System;

namespace HelixJump.Game.Interfaces
{
    public interface IPlayerInput
    {
        event Action EnableHitMode;
        event Action DisableHitMode;
        void Enable();
        void Disable();
    }
}