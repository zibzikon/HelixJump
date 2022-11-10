using System;
using System.Collections.Generic;
using System.Linq;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game.Arguments;
using HelixJump.Game.Interfaces;
using HelixJump.Game.Interfaces.Creators.Tower;

namespace HelixJump.Game.Factories
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
                throw new ApplicationException($"No tower layer part creator with type {towerLayerPartType}");
            return _ = towerLayerPartCreator.Create(towerLayerPartArguments);
        }
    }
}