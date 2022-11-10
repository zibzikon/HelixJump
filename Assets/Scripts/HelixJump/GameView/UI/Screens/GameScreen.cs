using System.Threading.Tasks;
using HelixJump.GameView.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace HelixJump.GameView.UI.Screens
{
    public class GameScreen : MonoBehaviour, IGameScreen
    {
        public Task<bool> ButtonPressedTask => _buttonPressedTaskCompletionSource.Task;
        private TaskCompletionSource<bool> _buttonPressedTaskCompletionSource = new ();

        [SerializeField] private Button button;
        
        public void Enable()
        {
            gameObject.SetActive(true);
            SubscribeEvents();
        }

        public void Disable()
        {
            UnSubscribeEvents();
            gameObject.SetActive(false);
        }

        private void SubscribeEvents()
        {
            button.onClick.AddListener(OnMoveNextLevelButtonPressed);
        }

        private void UnSubscribeEvents()
        {
            button.onClick.RemoveListener(OnMoveNextLevelButtonPressed);
        }

        private void OnMoveNextLevelButtonPressed()
        {
            _buttonPressedTaskCompletionSource.TrySetResult(true);
            _buttonPressedTaskCompletionSource = new();
        }
    }
}