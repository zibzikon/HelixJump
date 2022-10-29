using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelixJump.Core;
using HelixJump.Core.Enums.Tower;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Towers;
using HelixJump.Core.Towers.Layers;
using HelixJump.Core.Towers.Layers.Parts;
using HelixJump.Core.Utils;
using HelixJump.Game;
using HelixJump.Game.Input;
using HelixJump.Game.Interfaces;
using HelixJump.GameVIew.Builders;
using HelixJump.GameVIew.Containers;
using HelixJump.GameVIew.Views.Tower;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Resolution = HelixJump.Core.Utils.Resolution;

namespace HelixJump.Debug
{
    public class UnityDebug : MonoBehaviour, IGameObjectCreator
    {
        [Header("Tower")]
        
        [SerializeField] private bool createTower;
        
        [SerializeField] private int towerResolution;
        
        [SerializeField] private float towerLayersPadding;
        
        [Space]
        
        [Header("TowerLayerPart")]
        
        [FormerlySerializedAs("break")] [SerializeField] private bool breakTowerLayerPart;
        
        [FormerlySerializedAs("position")] [SerializeField] private Vector2Int towerLayerPosition;
        
        [Space]
        
        [Header("Builders")]
        [SerializeField] private TowerLayerPartViewBuilder towerLayerPartViewBuilder;
        [SerializeField] private TowerLayerViewBuilder towerLayerViewBuilder;
        [SerializeField] private TowerViewBuilder towerViewBuilder;

        [Space] 
        [Header("Containers")] [SerializeField]
        private TowerLayerPartViewPrefabsContainer towerLayerPartViewPrefabsContainer;
        
        [SerializeField] private PlayerViewBuilder playerViewBuilder;

        private PlayerInput _playerInput;

        private readonly List<IUpdatable> _contentToUpdate = new ();

        private ITower _testTower;
        
        private void Start()
        {
            towerLayerPartViewBuilder.Initialize(new GameObjectPool<TowerLayerPartView>(this,towerLayerPartViewPrefabsContainer.TowerLayerPartViews));
            towerLayerViewBuilder.Initialize(towerLayerPartViewBuilder);
            towerViewBuilder.Initialize(towerLayerViewBuilder);
        }
        
        private void Update()
        {
            _contentToUpdate.ForEach(updatable => updatable.OnUpdate());
            if (breakTowerLayerPart)
            {
                BreakTowerLayerPart();
                breakTowerLayerPart = false;
            }
            if (createTower)
            {
                CreateTestTower();
                
                createTower = false;
            }
        }

        private void BreakTowerLayerPart()
        {
           var towerLayers = _testTower.TowerLayers.ToArray();

           var towerLayer = towerLayers[towerLayerPosition.y];
           // var towerLayerPart = towerLayer.GetTowerLayerPartByPosition(_towerLayerPosition.x);
           // towerLayerPart.Break();
           towerLayer.Hit(new TowerLayerHitInfo(towerLayerPosition.x, null));
        }
        
        private async void CreateTestTower()
        {
            _contentToUpdate.Clear();
            var towerLayers = new Stack<ITowerLayer>();
            for (int i = 0; i < towerResolution; i++)
            {
                var towerLayerParts = new ITowerLayerPart[]
                {
                    new WeaklyTowerLayerPart(),
                    new WeaklyTowerLayerPart(),
                    new WeaklyTowerLayerPart(),
                    new UnbreakableTowerLayerPart(),
                    new WeaklyTowerLayerPart(),
                    new WeaklyTowerLayerPart(),
                    new WeaklyTowerLayerPart(),
                    new UnbreakableTowerLayerPart()
                };
                var towerLayer = new DefaultTowerLayer(new Resolution(8), new Rotation(i * 10), towerLayerParts);
                towerLayers.Push(towerLayer);
            }
                
            var tower = new GameTower(towerLayers, TowerType.Round, new Resolution(towerResolution));
            var towerView = await towerViewBuilder.GetAsync(tower, transform, towerLayersPadding);
            
            var player = new Player(tower, new TimeSpan(0, 0, 0, 0, 100), new TimeSpan(0, 0, 0, 0, 10));
            _playerInput = new PlayerInput(player);
            
            var playerView = playerViewBuilder.Get(player, towerLayersPadding, towerView.transform.position);
            player.StartMoving();
            
           _contentToUpdate.Add(_playerInput);
           _testTower = tower;
        }

        public T Create<T>(T prefab) where T : IGameObject
        {
            return (T)prefab.Instantiate();
        }
    }
}