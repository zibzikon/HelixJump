using System;
using System.Threading.Tasks;

namespace HelixJump.Core.Interfaces
{
    public interface IDestroyable
    {
        Task<bool> DestroyedTask { get; }
        void Destroy();
    }
}