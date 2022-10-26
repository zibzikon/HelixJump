using System.Collections.Generic;
using HelixJump.Core.Interfaces.Tower;
using UnityEngine;

namespace HelixJump.GamePlay.Views.Tower
{
    public class TowerView : ViewBase
    {
        [SerializeField] private Transform _pipe;
        
        [SerializeField] private Transform _towerLayersContainer;
        public Transform TowerLayersContainer => _towerLayersContainer; 

        private ITower _towerModel;
        
        public void Initialize(ITower towerModel, float layersPadding)
        {
            _towerModel = towerModel;
            UpdatePipeSize(layersPadding);
        }

        private void UpdatePipeSize(float layersPadding)
        {
            var localScale = _pipe.localScale;
            var yScale = (_towerModel.Resolution.Value * layersPadding);
            
            // idk whats is this number means, but its work 
            var magic = 5f;
            
            var defaultPipeScale = 1;
            _pipe.localScale = new Vector3(localScale.x, yScale * magic + defaultPipeScale, localScale.z);
        }
        
    }
}   