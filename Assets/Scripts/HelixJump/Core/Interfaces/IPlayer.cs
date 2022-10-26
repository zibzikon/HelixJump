using HelixJump.Core.Enums;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Core.Interfaces
{
    public interface IPlayer : IHittable
    {
        ITower BaseTower { get; }
        PlayerState State { get; }
        void DisableHitMode();
        void EnableHitMode();
        public void StartMoving();
        void StopMoving();
        int RowPosition { get; }
        float XPosition { get; }
    }
}