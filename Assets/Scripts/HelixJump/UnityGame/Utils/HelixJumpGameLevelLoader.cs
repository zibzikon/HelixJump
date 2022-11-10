using System.Threading.Tasks;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.GameView.Views.Tower;
using HelixJump.UnityGame.Interfaces;
using HelixJump.UnityGame.Interfaces.Arguments;
using UnityEngine;

namespace HelixJump.UnityGame.Utils
{
    public class HelixJumpGameLevelLoader : IUnityGameLevelLoader
    {
        private readonly IUnityGameTowerArguments _arguments;
        private readonly Transform _towerParentTransform;
        private readonly IGameObjectPool<TowerLayerPartView> _towerLayerPartViewsPool;

        public HelixJumpGameLevelLoader(IUnityGameTowerArguments arguments, Transform towerParentTransform,
            IGameObjectPool<TowerLayerPartView> towerLayerPartViewsPool)
        {
            _arguments = arguments;
            _towerParentTransform = towerParentTransform;
            _towerLayerPartViewsPool = towerLayerPartViewsPool;
        }

        public Task LoadLevelAsync(ITower tower)
        {
            return _arguments.TowerViewBuilder.GetAsync(tower, _towerParentTransform, _arguments.TowerLayersPadding);
        }
    }
}