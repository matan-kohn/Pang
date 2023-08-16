using UnityEngine;
using Zenject;

public class Game : MonoBehaviour
{
    [Inject]
    public void Setup(GameModel gameModel, GameView gameView, GameController gameController)
    {
        GameModel = gameModel;
        GameView = gameView;
        GameController = gameController;
    }

    public GameView GameView { get; private set; }
    public GameController GameController { get; private set; }
    public GameModel GameModel { get; private set; }
}
