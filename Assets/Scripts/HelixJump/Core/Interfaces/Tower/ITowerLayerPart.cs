using System;
using System.Threading.Tasks;

namespace HelixJump.Core.Interfaces.Tower
{
    public interface ITowerLayerPart : IHittable, IDestroyable
    {
        string Type { get; }
        event Action<ITowerLayerPart> Broken;
        void Break();
    }
}