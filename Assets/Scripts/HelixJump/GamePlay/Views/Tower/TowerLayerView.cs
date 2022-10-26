using System.Collections.Generic;
using System.Numerics;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.GamePlay.Factories;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace HelixJump.GamePlay.Views.Tower
{
    public class TowerLayerView : ViewBase
    {
        private TowerLayerPartViewFactory _towerLayerPartViewFactory;
        private ITowerLayer _towerLayerModel;
        private IEnumerable<TowerLayerPartView> _towerLayerPartViews;
        
        [SerializeField]
        private Transform _towerLayerPartsContainer;
        public Transform TowerLayerPartsContainer => _towerLayerPartsContainer;
        
        public void Initialize(ITowerLayer towerLayerModel,IEnumerable<TowerLayerPartView> towerLayerPartViews, Vector3 position)
        {
            _towerLayerPartViews = towerLayerPartViews;
            
            _towerLayerModel = towerLayerModel;
            
            UpdateTransform(position);
        }

        private void UpdateTransform(Vector3 position)
        {
            transform.localPosition = position;

            var localRotation = transform.localRotation;
            localRotation = Quaternion.Euler(localRotation.x, _towerLayerModel.Rotation.Value, localRotation.y);
            transform.localRotation = localRotation;
        }
        
    }
}