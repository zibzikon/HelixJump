using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HelixJump.Game;
using HelixJump.Game.Arguments;
using HelixJump.Game.Factories;
using HelixJump.Game.Interfaces;
using HelixJump.Game.Interfaces.Arguments;
using HelixJump.GameView.Views.Tower;
using HelixJump.UnityGame.Builders;
using HelixJump.UnityGame.Interfaces;
using HelixJump.UnityGame.Interfaces.Arguments;
using HelixJump.UnityGame.Utils;
using UnityEngine;

namespace HelixJump.UnityGame.Arguments
{
    public class UnityGameArguments : MonoBehaviour, IUnityGameArguments
    {
        public IUnityGameLevelLoader LevelLoader { get; private set; }

        public IGameArguments GameArguments { get; private set; }

        public IUnityGameTowerArguments UnityGameTowerArguments => unityGameTowerArguments;

        public IUnityGameUIArguments UnityGameUIArguments => unityGameUIArguments;

        public PlayerViewBuilder PlayerViewBuilder => playerViewBuilder;
        
        [SerializeField] private TowersArguments towersArguments;
        [SerializeField] private UnityGameTowerArguments unityGameTowerArguments;
        [SerializeField] private UnityGameUIArguments unityGameUIArguments;
        [SerializeField] private UnityPlayerArguments playerArguments;
        [SerializeField] private Transform objectsContainerTransform;
        [SerializeField] private PlayerViewBuilder playerViewBuilder;

        public void Initialize(TowerFactory towerFactory, IGameObjectPool<TowerLayerPartView> towerLayerPartViewsPool)
        {
            towersArguments.Initialize(towerFactory);
            unityGameTowerArguments.Initialize(towerLayerPartViewsPool, objectsContainerTransform);
            GameArguments = new GameArguments(towersArguments, playerArguments);
            LevelLoader = new HelixJumpGameLevelLoader(unityGameTowerArguments, objectsContainerTransform,
                towerLayerPartViewsPool);
        }
    }

    [Serializable]
    public struct UnityTowerLayerPartModifierArguments
    {
        public string Type => type;

        [SerializeField] private string type;
    }

    [Serializable]
    public struct UnityTowerLayerPartArguments
    {
        public string Type => type;
        public List<UnityTowerLayerPartModifierArguments> Modifiers => modifiers;

        [SerializeField] private string type;
        [SerializeField] private List<UnityTowerLayerPartModifierArguments> modifiers;
    }

    [Serializable]
    public struct UnityTowerLayerArguments
    {
        public string Type => type;
        public int Resolution => resolution;
        public float Rotation => rotation;
        public List<UnityTowerLayerPartArguments> Parts => parts;

        [SerializeField] private string type;
        [SerializeField] private int resolution;
        [SerializeField] private float rotation;
        [SerializeField] private List<UnityTowerLayerPartArguments> parts;
    }

    [Serializable]
    public struct UnityTowerArguments
    {
        public string Type => type;
        public int Capacity => capacity;
        public int Difficulty => difficulty;
        public float LayerRotationStep => layerRotationStep;
        public List<UnityTowerLayerArguments> TowerLayers => towerLayers;

        [SerializeField] private int difficulty;
        [SerializeField] private string type;
        [SerializeField] private int capacity;
        [SerializeField] private float layerRotationStep;
        [SerializeField] private List<UnityTowerLayerArguments> towerLayers;
    }

    public static class Extensions
    {
        public static TowerArguments ToTowerArguments(this UnityTowerArguments unityTowerArguments)
            => new()
            {
                Difficulty = unityTowerArguments.Difficulty.ToString(),
                Type = unityTowerArguments.Type,
                Capacity = unityTowerArguments.Capacity.ToString(),
                LayerRotationStep = unityTowerArguments.LayerRotationStep.ToString(CultureInfo.InvariantCulture),
                TowerLayers = unityTowerArguments.TowerLayers.Select(x => x.ToTowerLayerArguments()).ToList(),
            };

        public static TowerLayerArguments ToTowerLayerArguments(this UnityTowerLayerArguments arguments)
            => new()
            {
                Resolution = arguments.Resolution.ToString(),
                Type = arguments.Type,
                Parts = arguments.Parts.Select(x => x.ToTowerLayerPartArguments()).ToList(),
            };


        public static TowerLayerPartArguments ToTowerLayerPartArguments(this UnityTowerLayerPartArguments arguments)
            => new()
            {
                Type = arguments.Type,
                Modifiers = arguments.Modifiers.Select(x => x.ToTowerLayerPartModifierArguments()).ToList(),
            };


        public static TowerLayerPartModifierArguments ToTowerLayerPartModifierArguments(
            this UnityTowerLayerPartModifierArguments arguments)
            => new()
            {
                Type = arguments.Type,
            };
    }
}