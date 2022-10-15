
namespace HelixJump.Core.Interfaces.Tower
{
    public interface ITower : IDestroyable
    {
        public void RemoveTopTowerLayer();

        public ITowerLayer GetTopTowerLayer();
    }
};