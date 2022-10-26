
using System.Collections.Generic;
using System.Linq;
using HelixJump.Core.Enums.Tower;
using HelixJump.Core.Utils;

namespace HelixJump.Core.Interfaces.Tower
{
    public interface ITower : IDestroyable
    {
        void RemoveTopTowerLayer();
        TowerType Type { get; }
        Resolution Resolution { get; }
        IEnumerable<ITowerLayer> TowerLayers { get; }
        bool GetTopTowerLayer(out ITowerLayer resultTowerLayer);
    }
};