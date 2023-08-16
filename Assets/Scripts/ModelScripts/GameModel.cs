using System.Collections.Generic;
using Zenject;

public class GameModel : GameElement
{
    [Inject]
    public void Setup(UIModel uiModel, PlayerModel playerModel)
    {
        UIModel = uiModel;
        PlayerModel = playerModel;
        BallModels = new();
    }

    public UIModel UIModel { get; private set; }
    public PlayerModel PlayerModel { get; private set; }
    public List<BallModel> BallModels { get; private set; }
}
