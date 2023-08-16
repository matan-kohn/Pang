using System.Collections.Generic;
using Zenject;

public class GameView : GameElement
{
    [Inject]
    public void Setup(PlayerView playerView, StatsView statsView, ControlsView controlsView)
    {
        PlayerView = playerView;
        StatsView = statsView;
        ControlsView = controlsView;
    }

    public ControlsView ControlsView { get; private set; }
    public StatsView StatsView { get; private set; }
    public PlayerView PlayerView{ get; private set; }
    public List<BallView> BallViews { get; } = new();
    public List<BulletView> BulletViews { get; } = new();
}
