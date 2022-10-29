using System.Collections.Generic;
using System.Threading.Tasks;
using HelixJump.Core.Enums.Tower;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game.Extensions;
using HelixJump.GameVIew.Views.Tower;
using UnityEngine;

namespace HelixJump.GameVIew.Builders
{
    [CreateAssetMenu(fileName = "TowerLayerViewBuilder", menuName = "Builders/Tower/TowerLayerViewBuilder", order = 0)]
    public class TowerLayerViewBuilder : GameObjectBuilder
    {
        [SerializeField] private TowerLayerView towerLayerViewPrefab;
        private TowerLayerPartViewBuilder _towerLayerPartViewBuilder;

        public void Initialize(TowerLayerPartViewBuilder towerLayerPartViewBuilder)
        {
            _towerLayerPartViewBuilder = towerLayerPartViewBuilder;
        }
        
        public async Task<TowerLayerView> GetAsync(ITowerLayer towerLayer, TowerType towerType, Vector3 position, Transform parent)
        {
            var towerLayerView = CreateGameObject(towerLayerViewPrefab, parent);
            var towerLayerPartViews = new List<TowerLayerPartView>(towerLayer.Resolution.Value);
            var resolutionValue = towerLayer.Resolution.Value;
            
            for (int i = 0; i < resolutionValue; i++)
            {
                var towerLayerPart = towerLayer.GetTowerLayerPartByPosition(i);
                var partRotation = towerLayer.Resolution.GetPartRotation();
                var towerLayerPartRotation = partRotation  * i;
                var towerLayerPartView = await _towerLayerPartViewBuilder.GetAsync(towerLayerPart, towerType, towerLayerPartRotation, towerLayerView.transform);
                towerLayerPartViews.Add(towerLayerPartView);
            }
            
            towerLayerView.Initialize(towerLayer, towerLayerPartViews, position);
            
            return towerLayerView;
        }
    }
    
}