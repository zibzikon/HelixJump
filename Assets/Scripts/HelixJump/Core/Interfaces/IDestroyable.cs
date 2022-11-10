using System;
using System.Threading.Tasks;

namespace HelixJump.Core.Interfaces
{
    public interface IDestroyable
    {
        event Action<IDestroyable> Destroyed;
        void Destroy();
    }
}