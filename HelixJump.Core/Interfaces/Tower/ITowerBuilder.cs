using System.Threading.Tasks;

namespace HelixJump.Core.Interfaces.Tower
{
    public interface ITowerBuilder
    {
        public Task<ITower> BuildTowerAsync();
    }
}