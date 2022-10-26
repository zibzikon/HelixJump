using System;
using System.Threading;
using System.Threading.Tasks;
using HelixJump.Core;
using HelixJump.Core.Enums;
using HelixJump.Core.Interfaces;
using UnityEngine;

namespace HelixJump.GamePlay.Views
{
    public class PlayerView : ViewBase
    {
        [SerializeField] private string _blendJumpAndFallName;
        [SerializeField] private Animator _animator;

        private float _towerLayersPadding;
        private Vector3 _towerPosition;
        
        private IPlayer _player;

        private CancellationTokenSource _viewUpdateCancellationTokenSource = new();

        public void Initialize(IPlayer player, float towerLayersPadding, Vector3 towerPosition)
        {
            _player = player;
            _towerLayersPadding = towerLayersPadding;
            _towerPosition = towerPosition;
            StartUpdatingView();
        }

        private void OnDisable()
        {
            StopUpdatingView();
        }

        private void UpdatePlayerPosition()
        {
            var oneTurn = 360;
            transform.localRotation = Quaternion.Euler(0, _player.XPosition * oneTurn, 0);

            var yPosition = _player.RowPosition * _towerLayersPadding;
            transform.localPosition = new Vector3(_towerPosition.x, 
                yPosition, 
                _towerPosition.z);
        }
        
        private async void StartUpdatingView()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var lastPlayerState = _player.State;
            while (true)
            {
                await Task.Yield();
                if (_viewUpdateCancellationTokenSource.IsCancellationRequested)
                    return;
                
                if (lastPlayerState != _player.State) 
                {
                    cancellationTokenSource.Cancel();
                    cancellationTokenSource = new CancellationTokenSource();
                    
                    switch (_player.State)
                    {
                        case PlayerState.Hitting : StartFallingAnimation(cancellationTokenSource.Token); break;
                        case PlayerState.Staying : StartJumpingAnimation(cancellationTokenSource.Token); break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                
                UpdatePlayerPosition();
            }
        }

        public override void Disable()
        {
            base.Disable();
            StopUpdatingView();
        }

        private void StopUpdatingView()
        {
            _viewUpdateCancellationTokenSource.Cancel();
        }
        
        private async void StartJumpingAnimation(CancellationToken cancellationToken)
        {
            await SmoothlyChangeBlendValue(_blendJumpAndFallName, 1, 0, cancellationToken);
        }

        private async void StartFallingAnimation(CancellationToken cancellationToken)
        {
            await SmoothlyChangeBlendValue(_blendJumpAndFallName, 0, 1, cancellationToken);
        }

        private async Task SmoothlyChangeBlendValue(string name, float from, float to, CancellationToken cancellationToken)
        {
            var adder = 0.1f;
            var position = from;
            
            if (from > to)
                adder = -0.1f;
            
            while (from > to? position < to: position > to)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;
                
                await Task.Delay(10, cancellationToken);
                _animator.SetFloat(name, position);

                position += adder;
            }
        }
    }
}