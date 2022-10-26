using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelixJump.Core.Enums.Tower;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game.Extensions;
using HelixJump.GamePlay.Views.Tower;
using UnityEngine;

namespace HelixJump.GamePlay.Factories
{
    [CreateAssetMenu(fileName = "TowerLayerViewFactory", menuName = "Factories/Tower/TowerLayerViewFactory", order = 0)]
    public class TowerLayerViewFactory : GameObjectFactory
    {
        [SerializeField] private TowerLayerView _towerLayerViewPrefab;
        private TowerLayerPartViewFactory _towerLayerPartViewFactory;

        public void Initialize(TowerLayerPartViewFactory towerLayerPartViewFactory)
        {
            _towerLayerPartViewFactory = towerLayerPartViewFactory;
        }
        
        public async Task<TowerLayerView> GetAsync(ITowerLayer towerLayer, TowerType towerType, Vector3 position, Transform parent)
        {
            var towerLayerView = CreateGameObject(_towerLayerViewPrefab, parent);
            var towerLayerPartViews = new List<TowerLayerPartView>(towerLayer.Resolution.Value);
            var resolutionValue = towerLayer.Resolution.Value;
            
            for (int i = 0; i < resolutionValue; i++)
            {
                var towerLayerPart = towerLayer.GetTowerLayerPartByPosition(i);
                var towerLayerPartRotation = towerLayer.Resolution.GetPartRotation() * (i + 1);
                var towerLayerPartView = await _towerLayerPartViewFactory.GetAsync(towerLayerPart, towerType, towerLayerPartRotation, towerLayerView.transform);
                towerLayerPartViews.Add(towerLayerPartView);
            }
            
            towerLayerView.Initialize(towerLayer, towerLayerPartViews, position);
            
            return towerLayerView;
        }
    }
    
}