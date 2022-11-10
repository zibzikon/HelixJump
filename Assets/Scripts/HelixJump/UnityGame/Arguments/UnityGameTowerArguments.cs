using System.Collections.Generic;
using HelixJump.Game.Interfaces;
using HelixJump.GameView.Views.Tower;
using HelixJump.UnityGame.Builders;
using HelixJump.UnityGame.Containers;
using HelixJump.UnityGame.Interfaces;
using HelixJump.UnityGame.Interfaces.Arguments;
using UnityEngine;
using UnityEngine.Pool;

namespace HelixJump.UnityGame.Arguments
{
    [CreateAssetMenu(fileName = "UnityGameTowerArguments", menuName = "UnityGameArguments/UnityGameTowerArguments")]
    public class UnityGameTowerArguments : ScriptableObject, IUnityGameTowerArguments
    {
        public TowerViewBuilder TowerViewBuilder => towerViewBuilder;
        public TowerLayerViewBuilder TowerLayerViewBuilder => towerLayerViewBuilder;
        public TowerLayerPartViewBuilder TowerLayerPartViewBuilder => towerLayerPartViewBuilder;
        public IEnumerable<TowerLayerPartView> TowerLayerPartViewPrefabs => towerLayerPartViewPrefabsContainer.TowerLayerPartViews;
        public Transform TowerViewsContainerTransform { get; private set; }
        public float TowerLayersPadding => towerLayersPadding;

        [SerializeField] private TowerViewBuilder towerViewBuilder;
        [SerializeField] private TowerLayerViewBuilder towerLayerViewBuilder;
        [SerializeField] private TowerLayerPartViewBuilder towerLayerPartViewBuilder;
        [SerializeField] private TowerLayerPartViewPrefabsContainer towerLayerPartViewPrefabsContainer;
        
        [SerializeField] private float towerLayersPadding;
        private Transform _towerViewsContainerTransform;
        
        public void Initialize(IGameObjectPool<TowerLayerPartView> towerLayerPartViewsPool, Transform towerViewContainerTransform)
        {
            towerLayerPartViewBuilder.Initialize(towerLayerPartViewsPool);
            towerLayerViewBuilder.Initialize(towerLayerPartViewBuilder);
            towerViewBuilder.Initialize(towerLayerViewBuilder);
            TowerViewsContainerTransform = towerViewContainerTransform;
        }
    }
}