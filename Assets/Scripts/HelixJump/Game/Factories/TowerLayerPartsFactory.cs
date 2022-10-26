using System;
using System.Collections.Generic;
using System.Linq;
using HelixJump.Arguments;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Interfaces;

namespace HelixJump.Factories
{
    public class TowerLayerPartsFactory
    {
        private readonly Dictionary<string, ITowerLayerPartCreator> _creators;

        public TowerLayerPartsFactory(IEnumerable<ITowerLayerPartCreator> creators)
        {
            _creators = creators.ToDictionary(x => x.Type);
        }

        public ITowerLayerPart CreateTowerLayerPart(TowerLayerPartArguments towerLayerPartArguments)
        {
            var towerLayerPartType = towerLayerPartArguments.Type;
            if (!_creators.TryGetValue(towerLayerPartType, out var towerLayerPartCreator))
                throw new ApplicationException($"No tower layers with type {towerLayerPartType}");
            return _ = towerLayerPartCreator.Create(towerLayerPartArguments);
        }
    }
}