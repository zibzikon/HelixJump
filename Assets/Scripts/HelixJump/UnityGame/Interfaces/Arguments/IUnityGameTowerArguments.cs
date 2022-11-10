using System.Collections.Generic;
using HelixJump.GameView.Views.Tower;
using HelixJump.UnityGame.Builders;
using UnityEngine;

namespace HelixJump.UnityGame.Interfaces.Arguments
{
    public interface IUnityGameTowerArguments
    {
        TowerViewBuilder TowerViewBuilder { get; }
        TowerLayerViewBuilder TowerLayerViewBuilder { get; }
        TowerLayerPartViewBuilder TowerLayerPartViewBuilder { get; }
        
        IEnumerable<TowerLayerPartView> TowerLayerPartViewPrefabs { get; }

        Transform TowerViewsContainerTransform { get; }

        float TowerLayersPadding { get; }
    }
}