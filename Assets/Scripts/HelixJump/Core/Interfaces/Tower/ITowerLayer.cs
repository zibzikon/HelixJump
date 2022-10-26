using System;
using HelixJump.Core.Enums.Tower;
using HelixJump.Core.Utils;

namespace HelixJump.Core.Interfaces.Tower
{
    public interface ITowerLayer : IHittable, IDestroyable
    {
        Resolution Resolution { get; }
        Rotation Rotation { get; }
        ITowerLayerPart GetTowerLayerPartByPosition(int position);       
    }
}