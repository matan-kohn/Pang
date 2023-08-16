using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GeneralController : GameElement, IController
{
    void Start()
    {
        StartCoroutine(GameLoop());
    }

    /**
     * <summary>
     * the main game loop
     * responsible for increasing the levels
     * updates the game's data when levels increase 
     * </summary>
     */
    private IEnumerator GameLoop()
    {
        var currentLevel = Game.GameModel.UIModel.Level;
        var numberOfLevels = Game.GameModel.UIModel.NumberOfLevels;
        Game.GameController.BallController.CreateBalls(currentLevel);
        while (currentLevel < numberOfLevels && Game.GameModel.PlayerModel.Lives > 0)
        {
            if (Game.GameView.BallViews.Count == 0)
            {
                Game.GameController.BallController.RemoveAllBalls();
                Game.GameController.BulletController.RemoveAllBullets();
                currentLevel = Game.GameModel.UIModel.LevelUp();
                Game.GameController.BallController.CreateBalls(currentLevel);
                Game.GameView.StatsView.UpdateLevel();
            }

            yield return null;
        }

        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }

    
    /**
     * <summary>
     * takes a command's name and an optional additional data
     * and preforms the command
     * </summary>
     * <param name="commandType">an enum value containing a command name</param>
     * <param name="data">optional data for performing the command</param>
     */
    public void ProcessCommand(CommandType commandType, params object[] data)
    {
        switch (commandType)
        {
            case CommandType.Restart:
                Destroy(GameObject.FindWithTag(PangTags.ProjectContext));
                SceneManager.LoadScene("Menu");
                break;
        }
    }
}
