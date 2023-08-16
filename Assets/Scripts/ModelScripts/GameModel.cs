using System.Collections.Generic;

public class GameModel : GameElement
{
    public UIModel UIModel;
    public PlayerModel PlayerModel;
    public List<BallModel> BallModels = new();
}
