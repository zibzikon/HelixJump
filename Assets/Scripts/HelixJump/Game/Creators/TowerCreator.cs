using System;
using System.Collections.Generic;
using System.Linq;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Towers;
using HelixJump.Core.Utils;
using HelixJump.Game.Arguments;
using HelixJump.Game.Extensions;
using HelixJump.Game.Factories;
using HelixJump.Game.Interfaces.Creators.Tower;

namespace HelixJump.Game.Creators
{
    public class TowerCreator : ITowerCreator
    {
        private readonly TowerLayerFactory _towerLayerFactory;

        public string Type => "default";
        
        public TowerCreator(TowerLayerFactory towerLayerFactory)
        {
            _towerLayerFactory = towerLayerFactory;
        }
        
        public ITower Create(TowerArguments towerArguments)
        {
            var type = towerArguments.Type;
            var capacity = new Resolution(Int32.Parse(towerArguments.Capacity));
            var towerLayers = new Stack<ITowerLayer>();
            var rotationStep = new Rotation(towerArguments.LayerRotationStep.ToFloat());
            
            foreach (var towerLayerArguments in towerArguments.TowerLayers)
                towerLayers.Push(_towerLayerFactory.CreateTowerLayer(towerLayerArguments));
            

            return new GameTower(towerLayers, type, capacity, rotationStep);
        }
    }
}