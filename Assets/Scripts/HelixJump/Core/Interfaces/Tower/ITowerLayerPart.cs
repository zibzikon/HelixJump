using System;
using System.Threading.Tasks;
using HelixJump.Core.Enums.Tower;

namespace HelixJump.Core.Interfaces.Tower
{
    public interface ITowerLayerPart : IHittable, IDestroyable
    {
        TowerLayerPartType Type { get; }
        TaskCompletionSource<bool> BreakTaskCompletionSource { get; }
        void Break();
    }
}