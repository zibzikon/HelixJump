using System.Collections.Generic;
using System.Threading.Tasks;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.GameView.Views.Tower;
using UnityEngine;
using UnityEngine.Serialization;

namespace HelixJump.UnityGame.Builders
{
    [CreateAssetMenu(fileName = "TowerViewBuilder", menuName = "Builders/Tower/TowerViewBuilder", order = 0)]
    public class TowerViewBuilder : GameObjectBuilder
    {
        [FormerlySerializedAs("_towerViewPrefab")] [SerializeField] private TowerView towerViewPrefab;
        private TowerLayerViewBuilder _towerLayerViewBuilder;

        public void Initialize(TowerLayerViewBuilder towerLayerViewBuilder)
        {
            _towerLayerViewBuilder = towerLayerViewBuilder;
        }
        
        public async Task<TowerView> GetAsync(ITower tower, Transform parent, float layersPadding)
        {
            var towerLayerViews = new List<TowerLayerView>();
            var towerView = CreateGameObject(towerViewPrefab, parent);
            var towerLayersContainer = towerView.TowerLayersContainer;
            var towerLayerViewContainerPosition = towerLayersContainer.localPosition;
            var i = 0;
            foreach (var towerLayer in tower.TowerLayers)
            {
                var layerPosition = new Vector3(towerLayerViewContainerPosition.x,
                    towerLayerViewContainerPosition.y + layersPadding * i,
                    towerLayerViewContainerPosition.z);
                var towerLayerViewInstance = await _towerLayerViewBuilder.GetAsync(towerLayer, tower.Type, layerPosition, tower.RotationStep * i, towerLayersContainer);
                towerLayerViews.Add(towerLayerViewInstance);
                i++;
            }

            towerView.Initialize(tower, layersPadding);
            return towerView;
        }
    }
}