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
        public event Action RestartGameButtonPressed;
        public event Action MoveNextLevelButtonPressed;
        public event Action PauseGameButtonPressed;

        private bool _isInitialized;
        
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
            RegisterEvents();
        }

        private void OnDisable()
        {
            UnRegisterEvents();
        }

        private void RegisterEvents()
        {
            restartGameScreen.ButtonPressed += RestartGameButtonPressed;
            moveNextLevelGameScreen.ButtonPressed += MoveNextLevelButtonPressed;
            pauseGameScreen.ButtonPressed += PauseGameButtonPressed;

            _game.GameLose += OnGameLose;
            _game.GameWin += OnGameWin;
        }
        
        private void UnRegisterEvents()
        {
            restartGameScreen.ButtonPressed -= RestartGameButtonPressed;
            moveNextLevelGameScreen.ButtonPressed -= MoveNextLevelButtonPressed;
            pauseGameScreen.ButtonPressed -= PauseGameButtonPressed;
            
            _game.GameLose -= OnGameLose;
            _game.GameWin -= OnGameWin;
        }

        private void OnGameWin()
        {
            ChangeBaseScreen(moveNextLevelGameScreen);
        }

        private void OnGameLose()
        {
            ChangeBaseScreen(restartGameScreen);
        }

        private void ChangeBaseScreen(GameScreen gameScreen)
        {
            if (_enabledGameScreen is not null)
                _enabledGameScreen.Disable();

            _enabledGameScreen = gameScreen;
            _enabledGameScreen.Enable();
        }
        
    }
}