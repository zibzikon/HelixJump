using HelixJump.Game.Interfaces.Arguments;
using HelixJump.GameView.UI.Interfaces;
using HelixJump.UnityGame.Builders;

namespace HelixJump.UnityGame.Interfaces.Arguments
{
    public interface IUnityGameArguments
    {
        IUnityGameTowerArguments UnityGameTowerArguments { get; }
        IUnityGameLevelLoader LevelLoader { get; }
        IGameArguments GameArguments { get; }
        IUnityGameUIArguments UnityGameUIArguments { get; }
        PlayerViewBuilder PlayerViewBuilder { get; }

    }
}