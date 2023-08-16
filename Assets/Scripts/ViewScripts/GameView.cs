using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : GameElement
{
    public ControlsView ControlsView;
    public StatsView StatsView;
    public PlayerView PlayerView;
    public List<BallView> BallViews;
    public List<BulletView> BulletViews;
}
