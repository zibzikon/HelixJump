using System;
using System.Collections.Generic;
using HelixJump.Core.Utils;

namespace HelixJump.Core.Interfaces.Tower
{
    public interface ITower : IDestroyable
    {
        void RemoveTopTowerLayer();
        string Type { get; }
        Resolution Capacity { get; }
        Rotation RotationStep { get; }
        IEnumerable<ITowerLayer> TowerLayers { get; }
        event Action LayerDestroyed;
        bool GetTopTowerLayer(out ITowerLayer resultTowerLayer);
    }
};