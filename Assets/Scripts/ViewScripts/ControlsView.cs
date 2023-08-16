using DefaultNamespace;

public class ControlsView : GameElement
{
    /**
     * <summary>Handles a click on the left-arrow button</summary>
     */
    public void GoLeft()
    {
        var command = Game.GameModel.PlayerModel.PlayerState is PlayerState.Idle
            ? CommandType.GoLeft
            : CommandType.Stand;
        Game.GameController.PlayerController.ProcessCommand(command);
    }
    
    /**
     * <summary>Handles a click on the right-arrow button</summary>
     */
    public void GoRight()
    {
        var command = Game.GameModel.PlayerModel.PlayerState is PlayerState.Idle
            ? CommandType.GoRight
            : CommandType.Stand;
        Game.GameController.PlayerController.ProcessCommand(command);
    }

    /**
     * <summary>Handles a click on the shoot button</summary>
     */
    public void Shoot()
    {
        Game.GameController.BulletController.ProcessCommand(CommandType.Shoot);
    }

    /**
     * <summary>Handles a click on the restart button</summary>
     */
    public void Restart()
    {
        Game.GameController.GeneralController.ProcessCommand(CommandType.Restart);
    }
}
