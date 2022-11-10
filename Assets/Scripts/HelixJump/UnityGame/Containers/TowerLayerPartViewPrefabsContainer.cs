using System.Collections.Generic;
using HelixJump.GameView.Views.Tower;
using UnityEngine;

namespace HelixJump.UnityGame.Containers
{
    [CreateAssetMenu(fileName = "TowerLayerPartViewPrefabsContainer", menuName = "Containers/Tower/TowerLayerPartViewPrefabsContainer", order = 0)]
    public class TowerLayerPartViewPrefabsContainer : ScriptableObject
    {
        [SerializeField]private List<TowerLayerPartView> towerLayerPartViews;

        public IEnumerable<TowerLayerPartView> TowerLayerPartViews => towerLayerPartViews;
        
    }
}