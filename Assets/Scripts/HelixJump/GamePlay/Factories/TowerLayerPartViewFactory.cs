using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelixJump.Core.Enums.Tower;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;
using HelixJump.GamePlay.Views.Tower;
using UnityEngine;

namespace HelixJump.GamePlay.Factories
{
    [CreateAssetMenu(fileName = "TowerLayerPartViewFactory", menuName = "Factories/Tower/TowerLayerPartViewFactory", order = 0)]
    public class TowerLayerPartViewFactory : GameObjectFactory
    {
        [SerializeField]private List<TowerLayerPartViewBlank> towerLayerPartViewBlanks;

        private Dictionary<TowerType ,Dictionary<TowerLayerPartType, TowerLayerPartView>> _towerLayerPartViews;

        public void Initialize()
        {
            _towerLayerPartViews = new Dictionary<TowerType ,Dictionary<TowerLayerPartType, TowerLayerPartView>>();

            foreach (var towerLayerPartViewBlank in towerLayerPartViewBlanks)
            {
                var towerType = towerLayerPartViewBlank.TowerType;
                if (_towerLayerPartViews.TryGetValue(towerType, out var towerLayerPartViewsDictionary) == false)
                {
                    towerLayerPartViewsDictionary = new Dictionary<TowerLayerPartType, TowerLayerPartView>();
                    _towerLayerPartViews.Add(towerType, towerLayerPartViewsDictionary);
                }
                
                towerLayerPartViewsDictionary.Add(towerLayerPartViewBlank.Type, towerLayerPartViewBlank.TowerLayerPartViewPrefab);
            }
        }

        public Task<TowerLayerPartView> GetAsync(ITowerLayerPart towerLayerPart, TowerType type, Rotation rotation, Transform parent)
        {
            if (_towerLayerPartViews is null)
                throw new ApplicationException($"You trying to access {this.GetType()} before initialization");
            
            var towerLayerPartType = towerLayerPart.Type;
            
            if (_towerLayerPartViews.TryGetValue(type, out var towerLayerPartViewsDictionary) == false )
                throw new ApplicationException($"No tower layer part views with type {towerLayerPartType}");
            if(towerLayerPartViewsDictionary.TryGetValue(towerLayerPartType, out var towerLayerPartViewPrefab) == false)
                throw new ApplicationException($"No tower layer part views with Tower {towerLayerPartType}");

            var towerLayerPartView = CreateGameObject(towerLayerPartViewPrefab, parent);
            towerLayerPartView.Initialize(towerLayerPart, rotation);
            return Task.FromResult(towerLayerPartView);
            
        }


    }
    
    [Serializable]
    public class TowerLayerPartViewBlank
    {
        [SerializeField]
        private TowerType towerType;
        public TowerType TowerType => towerType;
        
        [SerializeField]
        private TowerLayerPartType type;
        public TowerLayerPartType Type => type;
        
        [SerializeField]
        private TowerLayerPartView towerLayerPartViewPrefab;
        public TowerLayerPartView TowerLayerPartViewPrefab => towerLayerPartViewPrefab;
    }
}