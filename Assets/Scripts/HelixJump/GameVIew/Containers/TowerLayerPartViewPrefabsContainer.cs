using System;
using System.Collections.Generic;
using HelixJump.Core.Enums.Tower;
using HelixJump.GameVIew.Views.Tower;
using UnityEngine;

namespace HelixJump.GameVIew.Containers
{
    [CreateAssetMenu(fileName = "TowerLayerPartViewPrefabsContainer", menuName = "Containers/Tower/TowerLayerPartViewPrefabsContainer", order = 0)]
    public class TowerLayerPartViewPrefabsContainer : ScriptableObject
    {
        [SerializeField]private List<TowerLayerPartView> towerLayerPartViews;

        public IEnumerable<TowerLayerPartView> TowerLayerPartViews => towerLayerPartViews;
        
    }
}