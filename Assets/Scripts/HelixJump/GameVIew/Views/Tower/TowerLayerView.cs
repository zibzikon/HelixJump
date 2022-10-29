using System.Collections.Generic;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.GameVIew.Builders;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace HelixJump.GameVIew.Views.Tower
{
    public class TowerLayerView : ViewBase
    {
        private TowerLayerPartViewBuilder _towerLayerPartViewBuilder;
        private ITowerLayer _towerLayerModel;
        private IEnumerable<TowerLayerPartView> _towerLayerPartViews;
        
        [SerializeField]
        private Transform towerLayerPartsContainer;
        public Transform TowerLayerPartsContainer => towerLayerPartsContainer;
        
        public void Initialize(ITowerLayer towerLayerModel,IEnumerable<TowerLayerPartView> towerLayerPartViews, Vector3 position)
        {
            _towerLayerPartViews = towerLayerPartViews;
            
            _towerLayerModel = towerLayerModel;
            
            CorrectTransform(position);
            Initialize();
        }

        private void CorrectTransform(Vector3 position)
        {
            transform.localPosition = position;

            var localRotation = transform.localRotation;
            localRotation = Quaternion.Euler(localRotation.x, _towerLayerModel.Rotation.Value, localRotation.y);
            transform.localRotation = localRotation;
        }
        
    }
}