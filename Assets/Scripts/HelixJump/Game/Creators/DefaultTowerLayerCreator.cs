using System;
using System.Linq;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Towers.Layers;
using HelixJump.Core.Utils;
using HelixJump.Game.Arguments;
using HelixJump.Game.Factories;
using HelixJump.Game.Interfaces;

namespace HelixJump.Game.Creators
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