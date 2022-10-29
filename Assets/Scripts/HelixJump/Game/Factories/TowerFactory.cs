using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game.Arguments;
using HelixJump.Game.Interfaces;

namespace HelixJump.Game.Factories
{
    public class TowerFactory
    {
        private readonly ITowerCreator _creator;

        public TowerFactory(ITowerCreator creator)
        {
            _creator = creator;
        }
        
        public ITower Get(TowerArguments towerArguments)
        {
            return _creator.Create(towerArguments);
        }
    }
}