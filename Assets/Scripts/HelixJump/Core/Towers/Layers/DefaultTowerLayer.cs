using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;

namespace HelixJump.Core.Towers.Layers
{
    public class DefaultTowerLayer : ITowerLayer
    {
        private readonly ITowerLayerPart[] _parts;
        public Resolution Resolution { get;  }
        private TaskCompletionSource<bool> _destroyedTaskCompletionSource = new (); 
        private readonly CancellationTokenSource _asyncMethodsAfterDestroyingCancellationTokenSource = new ();

        public Task<bool> DestroyedTask => _destroyedTaskCompletionSource.Task;
        public DefaultTowerLayer(Resolution resolution, IEnumerable<ITowerLayerPart> layerParts)
        {
            Resolution = resolution;

            var towerLayerParts = layerParts as ITowerLayerPart[] ?? layerParts.ToArray();
            
            if (towerLayerParts.Length != Resolution.Value)
                throw new IndexOutOfRangeException($"Enumerable: {layerParts.GetType()} does not match the tower resolution of tower {GetType()}");
            if (towerLayerParts.Any(x => x == null))
                throw new NullReferenceException($"Enumerable: {layerParts.GetType()} contains null value");
            
            _parts = towerLayerParts;
            
            foreach (var towerLayerPart in _parts)
                OnTowerLayerPartBreakAsync(towerLayerPart, _asyncMethodsAfterDestroyingCancellationTokenSource.Token);
        }

        private async void OnTowerLayerPartBreakAsync(ITowerLayerPart towerLayerPart, CancellationToken cancellationToken)
        {
            await towerLayerPart.BrokenTask;
            if (cancellationToken.IsCancellationRequested)
                return;
           
            Destroy();
        }

        public ITowerLayerPart GetTowerLayerPartByPosition(int position)
        {
            if (position >= Resolution.Value)
                throw new IndexOutOfRangeException("Position cannot be bigger than tower layer part resolution");

            return _parts[position];
        }


        public void Destroy()
        {
            _destroyedTaskCompletionSource.TrySetResult(true);
            
            foreach (var towerLayerPart in _parts)
                towerLayerPart.Destroy();
        }

        public void Hit(IHitInfo hitInfo)
        {
            if (hitInfo is not TowerLayerHitInfo towerLayerHitInfo)
                throw new ArgumentException("Invalid arguments");
            
            var hitPosition = towerLayerHitInfo.Position;
            var towerLayerPart = GetTowerLayerPartByPosition(hitPosition);
            towerLayerPart.Hit(hitInfo);
        }
    }
}