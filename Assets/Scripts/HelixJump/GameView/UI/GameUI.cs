using System;
using System.Threading;
using System.Threading.Tasks;
using HelixJump.Game.Interfaces;
using HelixJump.GameView.UI.Interfaces;
using HelixJump.GameView.UI.Screens;
using HelixJump.UnityGame.Interfaces.Arguments;
using UnityEngine;

namespace HelixJump.GameView.UI
{
    public class GameUI : MonoBehaviour, IGameUI
    {
        public Task<bool> RestartGameButtonPressedTask => restartGameScreen.ButtonPressedTask;
        public Task<bool> MoveNextLevelButtonPressedTask => moveNextLevelGameScreen.ButtonPressedTask;
        public Task<bool> PauseGameButtonPressedTask => pauseGameScreen.ButtonPressedTask;

        private bool _isInitialized;

        private CancellationTokenSource _cancellationTokenSource = new();

        [SerializeField] private GameScreen restartGameScreen;
        [SerializeField] private GameScreen moveNextLevelGameScreen;
        [SerializeField] private GameScreen pauseGameScreen;
        
        private IGame _game;
        private GameScreen _enabledGameScreen;
        
        public void Initialize(IGame game)
        {
            if (_isInitialized)
                throw new ApplicationException($"{GetType()} is already initialized but you trying to initialize it");
            
            _game = game;
            _isInitialized = true;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            var cancellationToken = _cancellationTokenSource.Token;
            
            OnTaskEnded(()=> _game.GameLoseTask, restartGameScreen, cancellationToken);
            OnTaskEnded(()=> _game.GameWinTask, moveNextLevelGameScreen, cancellationToken);
        }
        

        private async void OnTaskEnded<T> (Func<Task<T>> getTaskFunc, GameScreen screen, CancellationToken cancellationToken)
        {
            await getTaskFunc.Invoke();
            
            if (cancellationToken.IsCancellationRequested)
                return;
            
            if(_enabledGameScreen is not null) _enabledGameScreen.Disable();
            
            _enabledGameScreen = screen;
            screen.Enable();
            
            OnTaskEnded(getTaskFunc, screen, cancellationToken);
        }

    }
}