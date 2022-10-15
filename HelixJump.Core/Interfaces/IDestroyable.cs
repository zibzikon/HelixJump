using System;
using System.Threading.Tasks;

namespace HelixJump.Core.Interfaces
{
    public interface IDestroyable
    {
        public TaskCompletionSource<IDestroyable> DestroyedTaskCompletionSource { get; }
        public void Destroy();
    }
}