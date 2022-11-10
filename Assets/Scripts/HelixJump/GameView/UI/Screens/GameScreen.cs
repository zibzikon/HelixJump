using System;
using System.Threading.Tasks;
using HelixJump.GameView.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace HelixJump.GameView.UI.Screens
{
    public class GameScreen : MonoBehaviour, IGameScreen
    {
        public event Action ButtonPressed;

        [SerializeField] private Button button;
        
        public void Enable()
        {
            gameObject.SetActive(true);
            RegisterEvents();
        }

        public void Disable()
        {
            UnRegisterEvents();
            gameObject.SetActive(false);
        }

        private void RegisterEvents()
        {
            button.onClick.AddListener(OnMoveNextLevelButtonPressed);
        }

        private void UnRegisterEvents()
        {
            button.onClick.RemoveListener(OnMoveNextLevelButtonPressed);
        }

        private void OnMoveNextLevelButtonPressed()
        {
            ButtonPressed?.Invoke();
        }
    }
}