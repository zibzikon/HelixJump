using System;
using System.Threading.Tasks;
using HelixJump.Core.Enums.Tower;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;
using HelixJump.Game.Interfaces;
using HelixJump.GameVIew.Views.Tower;
using UnityEngine;

namespace HelixJump.GameVIew.Builders
{
    [CreateAssetMenu(fileName = "TowerLayerPartViewBuilder", menuName = "Builders/Tower/TowerLayerPartViewBuilder", order = 0)]
    public class TowerLayerPartViewBuilder : GameObjectBuilder
    {
        private IGameObjectPool<TowerLayerPartView> _objectPool;
        

        public void Initialize(IGameObjectPool<TowerLayerPartView> objectPool)
        {
            _objectPool = objectPool;
        }

        public Task<TowerLayerPartView> GetAsync(ITowerLayerPart towerLayerPart, TowerType type, Rotation rotation, Transform parent)
        {
            if (_objectPool is null)
                throw new ApplicationException($"You trying to access {this.GetType()} before initialization");
            
            var towerLayerPartType = towerLayerPart.Type;
            
            var towerLayerPartView = _objectPool.Get(x => x.TowerType == type && x.Type == towerLayerPartType);
            towerLayerPartView.transform.SetParent(parent);
            towerLayerPartView.Initialize(towerLayerPart, rotation);
            return Task.FromResult(towerLayerPartView);
        }


    }
}