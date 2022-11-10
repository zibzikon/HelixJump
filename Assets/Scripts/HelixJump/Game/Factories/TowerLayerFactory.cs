using System;
using System.Collections.Generic;
using System.Linq;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;
using HelixJump.Game.Arguments;
using HelixJump.Game.Interfaces;
using HelixJump.Game.Interfaces.Creators.Tower;

namespace HelixJump.Game.Factories
{
    public class TowerLayerFactory
    {
        private readonly ITowerLayerCreator _creator;

        public TowerLayerFactory(ITowerLayerCreator creator)
        {
            _creator = creator;
        }

        public ITowerLayer CreateTowerLayer(TowerLayerArguments towerLayerArguments)
        {
            return _creator.Create(towerLayerArguments);
        }
    }
}