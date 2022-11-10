using System.Threading.Tasks;

namespace HelixJump.Core.Interfaces.Tower
{
    public interface ITowerLayerPart : IHittable, IDestroyable
    {
        string Type { get; }
        Task<bool> BrokenTask { get; }
        void Break();
    }
}