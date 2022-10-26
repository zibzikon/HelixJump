using System.Collections.Generic;
using System.Threading.Tasks;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.GamePlay.Factories;
using HelixJump.GamePlay.Views;
using HelixJump.GamePlay.Views.Tower;
using UnityEngine;

namespace Factories
{
    [CreateAssetMenu(fileName = "TowerViewFactory", menuName = "Factories/Tower/TowerViewFactory", order = 0)]
    public class TowerViewFactory : GameObjectFactory
    {
        [SerializeField] private TowerView _towerViewPrefab;
        private TowerLayerViewFactory _towerLayerViewFactory;

        public void Initialize(TowerLayerViewFactory towerLayerViewFactory)
        {
            _towerLayerViewFactory = towerLayerViewFactory;
        }
        
        public async Task<TowerView> GetAsync(ITower tower, Transform parent, float layersPadding)
        {
            var towerLayerViews = new List<TowerLayerView>();
            var towerView = CreateGameObject(_towerViewPrefab, parent);
            var towerLayersContainer = towerView.TowerLayersContainer;
            var towerLayerViewContainerPosition = towerLayersContainer.localPosition;
            var i = 1;
            foreach (var towerLayer in tower.TowerLayers)
            {
                var layerPosition = new Vector3(towerLayerViewContainerPosition.x,
                    towerLayerViewContainerPosition.y + layersPadding * i,
                    towerLayerViewContainerPosition.z);
                var towerLayerViewInstance = await _towerLayerViewFactory.GetAsync(towerLayer, tower.Type, layerPosition, towerLayersContainer);
                towerLayerViews.Add(towerLayerViewInstance);
                i++;
            }

            towerView.Initialize(tower, layersPadding);
            return towerView;
        }
    }
}