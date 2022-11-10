using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game;
using HelixJump.Game.Interfaces;
using HelixJump.GameView.Views.Tower;
using HelixJump.UnityGame.Interfaces;
using HelixJump.UnityGame.Interfaces.Arguments;

namespace HelixJump.UnityGame
{
    public class HelixJumpUnityGame : IUnityGame
    {
        private readonly IGame _gameBase;
        private readonly IUnityGameArguments _arguments;
        private TowerView _currentTowerView;
       
        public HelixJumpUnityGame(IGame gameBase, IUnityGameArguments arguments)
        {
            _gameBase = gameBase;
            _arguments = arguments;
        }

        public async void Start()
        {
            _gameBase.Start();
            RegisterEvents();
            await GenerateTowerViewAsync();
            GeneratePlayerView();
        }
        
        private void RegisterEvents()
        {
            var gameUI = _arguments.UnityGameUIArguments.GameUI;

            gameUI.PauseGameButtonPressed += OnPauseGameButtonPressed;
            gameUI.RestartGameButtonPressed += OnRestartGameButtonPressed;
            gameUI.MoveNextLevelButtonPressed += OnMoveNextLevelButtonPressed;
        }
        
        private void UnRegisterEvents()
        {
            var gameUI = _arguments.UnityGameUIArguments.GameUI;

            gameUI.PauseGameButtonPressed -= OnPauseGameButtonPressed;
            gameUI.RestartGameButtonPressed -= OnRestartGameButtonPressed;
            gameUI.MoveNextLevelButtonPressed -= OnMoveNextLevelButtonPressed;
        }

        private void GeneratePlayerView()
        {
            _arguments.PlayerViewBuilder.Get(_gameBase.CurrentPlayer,
                _arguments.UnityGameTowerArguments.TowerLayersPadding, _currentTowerView.transform.position);
        }
        
        private async void OnGameBaseLevelChanged()
        {
           await GenerateTowerViewAsync();
        }

        private async Task GenerateTowerViewAsync()
        {
            var towerArguments = _arguments.UnityGameTowerArguments;
            var towerView = await towerArguments.TowerViewBuilder.GetAsync(_gameBase.CurrentTower, towerArguments.TowerViewsContainerTransform, towerArguments.TowerLayersPadding);
            _currentTowerView = towerView;
        }
        
        private void OnRestartGameButtonPressed()
        {
            _gameBase.RestartLevel();
        }
        
        private void OnPauseGameButtonPressed()
        {
            _gameBase.Pause();
        }
        
        private void OnMoveNextLevelButtonPressed()
        {
            _gameBase.MoveNextLevel();
        }
    }
}