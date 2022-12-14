using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;

namespace HelixJump.Core.Towers
{
    public class GameTower : ITower
    {
        public Resolution Capacity { get; private set; }
        
        public Rotation RotationStep { get; }

        public IEnumerable<ITowerLayer> TowerLayers => _towerLayers;
        
        public string Type { get; }

        public Task<ITower> LayerDestroyedTaskChangedTask => _layerDestroyedTaskCompletionSource.Task;

        public Task<bool> DestroyedTask => _destroyedTaskCompletionSource.Task;
        
        private readonly TaskCompletionSource<bool> _destroyedTaskCompletionSource = new();
        private readonly TaskCompletionSource<ITower> _layerDestroyedTaskCompletionSource = new();
        
        private readonly Stack<ITowerLayer> _towerLayers;
        
        public GameTower(Stack<ITowerLayer> towerLayers, string type, Resolution capacity, Rotation rotationStep)
        {
            Type = type;
            Capacity = capacity;
            RotationStep = rotationStep;
            _towerLayers = new Stack<ITowerLayer>();
            
            if (towerLayers.Any(x => x == null))
                throw new NullReferenceException($"Enumerable: {towerLayers} contains null value");

            _towerLayers = towerLayers;
            
            foreach (var towerLayer in _towerLayers)
                OnTowerLayerDestroyed(towerLayer);
            
        }

        private async void OnTowerLayerDestroyed(ITowerLayer towerLayer)
        {
            await towerLayer.DestroyedTask;

            if (_towerLayers.Any() == false)
            {
                Destroy();
                return;;
            }
            
            if (towerLayer != _towerLayers.Peek())
                throw new InvalidProgramException($"Tower layer:{towerLayer} in tower {this} cannot be destroyed, because is not top layer");

            _towerLayers.Pop();
            Capacity = new Resolution(Capacity.Value - 1);
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
           _destroyedTaskCompletionSource.SetResult(true);
        }
    }
}