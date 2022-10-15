using System;

namespace HelixJump.Core.Interfaces.Tower
{
    public interface ITowerLayer : IHittable, IDestroyable
    {
        public Resolution Resolution { get; }
        public Rotation Rotation { get; }
        public ITowerLayerPart GetTowerLayerPartByPosition(int position);       
    }
}