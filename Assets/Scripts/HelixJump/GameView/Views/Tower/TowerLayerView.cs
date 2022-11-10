using System.Collections.Generic;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;
using HelixJump.UnityGame.Builders;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace HelixJump.GameView.Views.Tower
{
    public class TowerLayerView : ViewBase
    {
        private TowerLayerPartViewBuilder _towerLayerPartViewBuilder;
        private ITowerLayer _towerLayerModel;
        private IEnumerable<TowerLayerPartView> _towerLayerPartViews;
        
        [SerializeField]
        private Transform towerLayerPartsContainer;
        public Transform TowerLayerPartsContainer => towerLayerPartsContainer;
        
        public void Initialize(ITowerLayer towerLayerModel, IEnumerable<TowerLayerPartView> towerLayerPartViews, Vector3 position, Rotation rotation)
        {
            _towerLayerPartViews = towerLayerPartViews;
            
            _towerLayerModel = towerLayerModel;
            
            CorrectTransform(position, rotation);
            Enable();
        }

        private void CorrectTransform(Vector3 position, Rotation rotation)
        {
            transform.localPosition = position;

            var localRotation = transform.localRotation;
            localRotation = Quaternion.Euler(localRotation.x, rotation.Value, localRotation.y);
            transform.localRotation = localRotation;
        }
        
    }
}