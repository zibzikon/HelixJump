using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelixJump.Core.Enums.Tower;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;

namespace HelixJump.Core.Towers
{
    public class GameTower : ITower
    {
        private readonly Stack<ITowerLayer> _towerLayers;
        
        public Resolution Resolution { get; }
        public IEnumerable<ITowerLayer> TowerLayers => _towerLayers;
        public TowerType Type { get; }

        public TaskCompletionSource<bool> DestroyedTaskCompletionSource { get; } = new();
        public GameTower(Stack<ITowerLayer> towerLayers, TowerType type, Resolution resolution)
        {
            Type = type;
            Resolution = resolution;
            _towerLayers = new Stack<ITowerLayer>();

            
            
            if (towerLayers.Any(x => x == null))
                throw new NullReferenceException($"Enumerable: {towerLayers} contains null value");

            _towerLayers = towerLayers;
        }

        private async void OnTowerLayerDestroyed(ITowerLayer towerLayer)
        {
            await towerLayer.DestroyedTaskCompletionSource.Task;

            if (_towerLayers.Any() == false)
            {
                Destroy();
                return;;
            }
            
            if (towerLayer != _towerLayers.Peek())
                throw new InvalidProgramException($"Tower layer:{towerLayer} in tower {this} cannot be destroyed, because is not top layer");

            _towerLayers.Pop();
            
            if (_towerLayers.Any() == false)
                Destroy();
        }

        public void RemoveTopTowerLayer()
        {
            _ = _towerLayers.Pop();
        }


        public bool GetTopTowerLayer(out ITowerLayer resultTowerLayer)
        {
            return _towerLayers.TryPeek(out resultTowerLayer);
        }

        public void Destroy()
        {
           DestroyedTaskCompletionSource.SetResult(true);
        }
    }
}