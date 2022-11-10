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
        private CancellationTokenSource _disableGameCancellationTokenSource = new ();
       
        public HelixJumpUnityGame(IGame gameBase, IUnityGameArguments arguments)
        {
            _gameBase = gameBase;
            _arguments = arguments;
        }

        public async void Start()
        {
            _gameBase.Start();
            SubscribeEvents();
            await GenerateTowerViewAsync();
            GeneratePlayerView();
        }
        
        private void SubscribeEvents()
        {
            var gameUI = _arguments.UnityGameUIArguments.GameUI;

            var cancellationToken = _disableGameCancellationTokenSource.Token;
            OnTaskEnded(()=> _gameBase.GameTowerChangedTask, OnGameBaseLevelChanged, cancellationToken);
            
            OnTaskEnded(()=> gameUI.RestartGameButtonPressedTask, OnRestartGameButtonPressed, cancellationToken);
            OnTaskEnded(()=> gameUI.PauseGameButtonPressedTask, OnPauseGameButtonPressed,cancellationToken);
            OnTaskEnded(()=> gameUI.MoveNextLevelButtonPressedTask, OnMoveNextLevelButtonPressed, cancellationToken);
           
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
        
        private async void OnTaskEnded<T> (Func<Task<T>> getTaskFunc, Action action, CancellationToken cancellationToken)
        {
            await getTaskFunc.Invoke();
            
            if (cancellationToken.IsCancellationRequested)
                return;
            
            action();
            
            OnTaskEnded(getTaskFunc, action, cancellationToken);
        }

    }
}