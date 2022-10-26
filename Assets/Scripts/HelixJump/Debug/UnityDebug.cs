using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Factories;
using HelixJump.Core;
using HelixJump.Core.Enums.Tower;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Towers;
using HelixJump.Core.Towers.Layers;
using HelixJump.Core.Towers.Layers.Parts;
using HelixJump.Core.Utils;
using HelixJump.Game.Input;
using HelixJump.GamePlay.Factories;
using HelixJump.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Resolution = HelixJump.Core.Utils.Resolution;

namespace HelixJump.Debug
{
    public class UnityDebug : MonoBehaviour
    {
        [SerializeField] private bool _createTower;

        [SerializeField]
        private int _towerResolution;
        
        [SerializeField]
        private float _towerLayersPadding;

        [SerializeField] private TowerLayerPartViewFactory _towerLayerPartViewFactory;
        [SerializeField] private TowerLayerViewFactory _towerLayerViewFactory;
        [SerializeField] private TowerViewFactory _towerViewFactory;
        
        [SerializeField] private PlayerViewFactory _playerViewFactory;

        private PlayerInput _playerInput;

        private TaskCompletionSource<bool> _test = new();

        private CancellationTokenSource _testStop = new();

        private readonly List<IUpdatable> _contentToUpdate = new ();
        
        private void Start()
        {
            _towerLayerPartViewFactory.Initialize();
            _towerLayerViewFactory.Initialize(_towerLayerPartViewFactory);
            _towerViewFactory.Initialize(_towerLayerViewFactory);
        }

        private async void StartWaitingForTaskCompletion(CancellationToken cancellationToken)
        {

            while (!cancellationToken.IsCancellationRequested)
            {
                UnityEngine.Debug.Log("Hello");
                await Task.Delay(100);
            }    
        }
        private void Update()
        {
            _contentToUpdate.ForEach(updatable => updatable.OnUpdate());
            
            if (!_createTower) return;
            CreateTestTower();
            _createTower = false;
        }

        private async void CreateTestTower()
        {
            _contentToUpdate.Clear();
            var towerLayers = new Stack<ITowerLayer>();
            for (int i = 0; i < _towerResolution; i++)
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
                
            var tower = new GameTower(towerLayers, TowerType.Round, new Resolution(_towerResolution));
            var towerView = await _towerViewFactory.GetAsync(tower, transform, _towerLayersPadding);
            
            var player = new Player(tower, new TimeSpan(0, 0, 0, 0, 100), new TimeSpan(0, 0, 0, 0, 10));
            _playerInput = new PlayerInput(player);
            
            var playerView = _playerViewFactory.Get(player, _towerLayersPadding, towerView.transform.position);
            player.StartMoving();
            
           _contentToUpdate.Add(_playerInput);
        }
    }
}