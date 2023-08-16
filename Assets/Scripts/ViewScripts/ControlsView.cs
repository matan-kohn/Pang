using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ControlsView : GameElement
{
    public void GoLeft()
    {
        var command = Game.GameModel.PlayerModel.PlayerState is PlayerState.Idle
            ? CommandType.GoLeft
            : CommandType.Stand;
        Game.GameController.ProcessCommand(command);
    }
    
    public void GoRight()
    {
        var command = Game.GameModel.PlayerModel.PlayerState is PlayerState.Idle
            ? CommandType.GoRight
            : CommandType.Stand;
        Game.GameController.ProcessCommand(command);
    }

    public void Shoot()
    {
        Game.GameController.ProcessCommand(CommandType.Shoot);
    }

    public void Restart()
    {
        Game.GameController.ProcessCommand(CommandType.Restart);
    }
}
