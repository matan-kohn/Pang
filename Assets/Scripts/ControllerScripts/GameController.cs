using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameController : GameElement
{
    protected Dictionary<ControllerType, IController> Controllers;

    [Inject]
    public void Setup(GeneralController gameController, PlayerController playerController,
        BallController ballController,
        BulletController bulletController)
    {
        Controllers.Add(ControllerType.GeneralController, gameController);
        Controllers.Add(ControllerType.PlayerController, gameController);
        Controllers.Add(ControllerType.BallController, gameController);
        Controllers.Add(ControllerType.BulletController, gameController);
    }

    public GeneralController GeneralController => Controllers[ControllerType.GeneralController] as GeneralController;
    public PlayerController PlayerController => Controllers[ControllerType.PlayerController] as PlayerController;
    public BallController BallController => Controllers[ControllerType.BallController] as BallController;
    public BulletController BulletController => Controllers[ControllerType.BulletController] as BulletController;
}
