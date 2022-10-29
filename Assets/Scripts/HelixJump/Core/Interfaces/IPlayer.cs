using HelixJump.Core.Enums;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;

namespace HelixJump.Core.Interfaces
{
    public interface IPlayer : IHittable
    {
        Score Score { get; }
        ITower BaseTower { get; }
        PlayerState State { get; }
        Position Position { get; }
        void DisableHitMode();
        void EnableHitMode();
        void ChangeBaseTower(ITower tower);
        void StartMoving();
        void StopMoving();
        void AddScore(int score);
        void RemoveScore(int score);
    }
}