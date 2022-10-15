using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Core.Towers
{
    public class GameTower : ITower
    {
        private readonly Stack<ITowerLayer> _towerLayers;

        public TaskCompletionSource<IDestroyable> DestroyedTaskCompletionSource { get; } = new();
        public GameTower(IEnumerable<ITowerLayer> towerLayers)
        {
            _towerLayers = new Stack<ITowerLayer>();

            var enumerable = towerLayers as ITowerLayer[] ?? towerLayers.ToArray();
            
            if (enumerable.Any(x => x == null))
                throw new NullReferenceException($"Enumerable: {towerLayers} contains null value");
            
            foreach (var towerLayer in enumerable)
            {
                _towerLayers.Push(towerLayer);
                OnTowerLayerDestroyed(towerLayer);
            }
        }

        private async void OnTowerLayerDestroyed(ITowerLayer towerLayer)
        {
            await towerLayer.DestroyedTaskCompletionSource.Task;

            if (towerLayer != _towerLayers.Peek())
                throw new InvalidProgramException($"Tower layer:{towerLayer} in tower {this} cannot be destroyed, because is not top layer");

            _towerLayers.Pop();
        }

        public void RemoveTopTowerLayer()
        {
            _ = _towerLayers.Pop();
        }

        public ITowerLayer GetTopTowerLayer()
        {
            return _ = _towerLayers.Peek();
        }

        public void Destroy()
        {
           DestroyedTaskCompletionSource.SetResult(this);
        }
    }
}