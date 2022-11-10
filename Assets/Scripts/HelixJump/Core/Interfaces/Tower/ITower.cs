using System.Collections.Generic;
using System.Threading.Tasks;
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
        Task<ITower> LayerDestroyedTaskChangedTask { get; }
        bool GetTopTowerLayer(out ITowerLayer resultTowerLayer);
    }
};