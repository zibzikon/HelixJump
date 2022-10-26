using System;
using System.Collections.Generic;
using System.Linq;
using HelixJump.Arguments;
using HelixJump.Core;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Towers.Layers;
using HelixJump.Core.Utils;
using HelixJump.Factories;
using HelixJump.Interfaces;

namespace HelixJump.Creators
{
    public class DefaultTowerLayerCreator : ITowerLayerCreator
    {
        private readonly TowerLayerPartsFactory _towerLayerPartsFactory;
        public string Type => "default";
        
        public DefaultTowerLayerCreator(TowerLayerPartsFactory towerLayerPartsFactory)
        {
            _towerLayerPartsFactory = towerLayerPartsFactory;
        }
        
        public ITowerLayer Create(TowerLayerArguments towerLayerArguments)
        {
            var partsArguments = towerLayerArguments.Parts;
            var resolution = new Resolution(Int32.Parse(towerLayerArguments.Resolution));
            var rotation = new Rotation(Int32.Parse(towerLayerArguments.Rotation));
            var partsList = partsArguments.Select(partArguments => _towerLayerPartsFactory.CreateTowerLayerPart(partArguments));
            
            return new DefaultTowerLayer(resolution, rotation, partsList);
        }
    }
}