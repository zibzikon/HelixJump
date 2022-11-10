using HelixJump.Core.Utils;

namespace HelixJump.Core.Interfaces.Tower
{
    public interface ITowerLayer : IHittable, IDestroyable
    {
        Resolution Resolution { get; }
        ITowerLayerPart GetTowerLayerPartByPosition(int position);       
    }
}