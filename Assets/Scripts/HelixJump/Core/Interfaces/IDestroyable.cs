using System;
using System.Threading.Tasks;

namespace HelixJump.Core.Interfaces
{
    public interface IDestroyable
    {
        TaskCompletionSource<bool> DestroyedTaskCompletionSource { get; }
        void Destroy();
    }
}