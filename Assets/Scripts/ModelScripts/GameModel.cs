using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : GameElement
{
    public UIModel UIModel;
    public PlayerModel PlayerModel;
    public List<BallModel> BallModels = new();
}
