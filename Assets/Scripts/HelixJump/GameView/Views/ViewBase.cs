using System;
using System.Threading.Tasks;
using HelixJump.Game.Interfaces;
using UnityEngine;

namespace HelixJump.GameView.Views
{
    public abstract class ViewBase : MonoBehaviour, IGameObject
    {
        public event Action<IGameObject> ResetAndDisabled;
        
        public bool Disabled { get; private set; }

        public virtual void Enable()
        {
            gameObject.SetActive(true);
        }
        
        public IGameObject Instantiate()
            => Instantiate(this);

        public virtual void ResetAndDisable()
        {
            gameObject.SetActive(false);
            
            Disabled = true;
            ResetAndDisabled?.Invoke(this);
        }
    }
}