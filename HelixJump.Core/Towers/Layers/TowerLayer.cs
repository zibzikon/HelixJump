using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;

namespace HelixJump.Core.Towers.Layers
{
    public class TowerLayer : ITowerLayer
    {
        private readonly ITowerLayerPart[] _parts;
        public Resolution Resolution { get;  }
        public TaskCompletionSource<IDestroyable> DestroyedTaskCompletionSource { get; protected set; }  = new (); 
        public Rotation Rotation { get; }
        private readonly CancellationTokenSource _asyncMethodsAfterDestroyingCancellationTokenSource = new ();
        
        public TowerLayer(Resolution resolution, Rotation rotation, IEnumerable<ITowerLayerPart> layerParts)
        {
            Resolution = resolution;
            Rotation = rotation;
            
            var towerLayerParts = layerParts as ITowerLayerPart[] ?? layerParts.ToArray();
            
            if (towerLayerParts.Length != Resolution.Value)
                throw new IndexOutOfRangeException($"Enumerable: {layerParts.GetType()} does not match the tower resolution of tower {GetType()}");
            if (towerLayerParts.Any(x => x == null))
                throw new NullReferenceException($"Enumerable: {layerParts.GetType()} contains null value");
            
            _parts = towerLayerParts;
            
            foreach (var towerLayerPart in _parts)
                OnTowerLayerPartDestroyed(towerLayerPart, _asyncMethodsAfterDestroyingCancellationTokenSource.Token);
        }

        private async void OnTowerLayerPartDestroyed(ITowerLayerPart towerLayerPart, CancellationToken cancellationToken)
        {
            await towerLayerPart.DestroyedTaskCompletionSource.Task;
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
            DestroyedTaskCompletionSource.SetResult(this);
            _asyncMethodsAfterDestroyingCancellationTokenSource.Cancel();
            
            foreach (var towerLayerPart in _parts)
                towerLayerPart.Destroy();
        }

        public void Hit(IHitInfo hitInfo)
        {
            if (hitInfo is not TowerLayerHitInfo towerLayerHitInfo)
                throw new ArgumentException("Invalid arguments");
            
            var hitPosition = towerLayerHitInfo.Position;
            var towerLayer = GetTowerLayerPartByPosition(hitPosition);
            towerLayer.Hit(hitInfo);
        }
    }
}