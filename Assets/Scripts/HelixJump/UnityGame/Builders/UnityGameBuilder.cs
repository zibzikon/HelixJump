using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelixJump.Core;
using HelixJump.Core.Enums;
using HelixJump.Core.Interfaces;
using HelixJump.Game;
using HelixJump.Game.Builders;
using HelixJump.Game.Controllers;
using HelixJump.Game.Creators;
using HelixJump.Game.Factories;
using HelixJump.Game.Interfaces;
using HelixJump.Game.Interfaces.Creators.Tower;
using HelixJump.GameView.Views.Tower;
using HelixJump.UnityGame.Arguments;
using HelixJump.UnityGame.Input;
using HelixJump.UnityGame.Interfaces;
using HelixJump.UnityGame.Utils;
using UnityEngine;

namespace HelixJump.UnityGame.Builders
{
    internal class UnityGameBuilder : MonoBehaviour, IUnityGameBuilder
    {
        [SerializeField]private UnityGameArguments arguments;
        [SerializeField] private bool _do;
        private IPlayer _player;
        
        private void Update()
        {
            if (_do)
            {
                if (_player.State == PlayerState.Staying)
                    _player.EnableHitMode();
                else
                    _player.DisableHitMode(); 
                
                _do = false;
            }
        }

        public async Task<IUnityGame> BuildAsync()
        {
            var towerLayerPartViewsPool = await CreateTowerLayerPartViewGameObjectPoolAsync();
            arguments.Initialize(CreateTowerFactory(), towerLayerPartViewsPool);
            
            var gameArguments = arguments.GameArguments;
            var playerArguments = gameArguments.PlayerArguments;
            var movingInterval = new TimeSpan(0, 0, 0, 0, playerArguments.MovingInterval);
            var hitInterval = new TimeSpan(0, 0, 0, 0, playerArguments.HitInterval);
            var player = new Player(hitInterval, movingInterval);
            _player = player;
            var towerArguments = gameArguments.TowersArguments;
            var towerBuilder = new RandomTowerBuilder(towerArguments.TowerFactory, towerArguments.TowerArgumentsCollection);
            var playerInput = new PlayerInput();
            var playerController = new PlayerController(player, playerInput);
            var game = new HelixJumpGame(player, playerController, towerBuilder);
            var unityGame = new HelixJumpUnityGame(game, arguments);
            
            return unityGame;
        }

        private async Task<IGameObjectPool<TowerLayerPartView>> CreateTowerLayerPartViewGameObjectPoolAsync()
        {
            var objects = new List<TowerLayerPartView>();
            var oneGenerationCount = 500;
            var creator = new UnityObjectsCreator();
            var towerLayerPartViewPrefabs = arguments.UnityGameTowerArguments.TowerLayerPartViewPrefabs;
            var prefabs = towerLayerPartViewPrefabs as TowerLayerPartView[] ?? towerLayerPartViewPrefabs.ToArray();

            foreach (var prefab in prefabs)
                objects.AddRange(await GeneratePoolObjectsAsync(creator, prefab, oneGenerationCount));

            return new GameObjectPool<TowerLayerPartView>(creator, prefabs, objects);
        }

        private async Task<IEnumerable<TowerLayerPartView>> GeneratePoolObjectsAsync(IGameObjectCreator creator,
            TowerLayerPartView prefab, int count)
        {
            var objects = new List<TowerLayerPartView>();
            for (int i = 0; i < count; i++)
                objects.Add(creator.Create(prefab));

            return objects;
        }

        private TowerFactory CreateTowerFactory()
        {
            var towerLayerPartsModifiersFactory = new TowerLayerPartModifierFactory(null);
            
            var towerLayerPartsCreators = new ITowerLayerPartCreator[] 
            {new UnbreakableLayerPartsCreator(towerLayerPartsModifiersFactory),
                new WeaklyLayerPartsCreator(towerLayerPartsModifiersFactory) };
            
            var towerLayerPartsFactory = new TowerLayerPartsFactory(towerLayerPartsCreators);
            var towerLayerCreator = new DefaultTowerLayerCreator(towerLayerPartsFactory);
            var towerLayerFactory = new TowerLayerFactory(towerLayerCreator);
            var towerCreator = new TowerCreator(towerLayerFactory);
            var towerFactory = new TowerFactory(towerCreator);

            return towerFactory;
        }
    }
}