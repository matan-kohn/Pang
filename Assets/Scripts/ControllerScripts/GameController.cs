using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameController : GameElement
{
    protected Dictionary<ControllerType, IController> Controllers = new();

    [Inject]
    public void Setup(GeneralController generalController, PlayerController playerController,
        BallController ballController,
        BulletController bulletController)
    {
        Controllers.Add(ControllerType.GeneralController, generalController);
        Controllers.Add(ControllerType.PlayerController, playerController);
        Controllers.Add(ControllerType.BallController, ballController);
        Controllers.Add(ControllerType.BulletController, bulletController);
    }

    public GeneralController GeneralController => Controllers[ControllerType.GeneralController] as GeneralController;
    public PlayerController PlayerController => Controllers[ControllerType.PlayerController] as PlayerController;
    public BallController BallController => Controllers[ControllerType.BallController] as BallController;
    public BulletController BulletController => Controllers[ControllerType.BulletController] as BulletController;
}
