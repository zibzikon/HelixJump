using HelixJump.Core.Interfaces.Tower;
using UnityEngine;

namespace HelixJump.GameVIew.Views.Tower
{
    public class TowerView : ViewBase
    {
        [SerializeField] private Transform pipe;
        
        [SerializeField] private Transform towerLayersContainer;
        public Transform TowerLayersContainer => towerLayersContainer; 

        private ITower _towerModel;
        
        public void Initialize(ITower towerModel, float layersPadding)
        {
            _towerModel = towerModel;
            UpdatePipeSize(layersPadding);
            Initialize();
        }

        private void UpdatePipeSize(float layersPadding)
        {
            var localScale = pipe.localScale;
            var yScale = (_towerModel.Capacity.Value * layersPadding);
            
            // id`k whats is this number means, but its work 
            var magic = 5f;
            
            var defaultPipeScale = 1;
            pipe.localScale = new Vector3(localScale.x, yScale * magic + defaultPipeScale, localScale.z);
        }
        
    }
}   