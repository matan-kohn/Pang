using System;
using DefaultNamespace;
using UnityEngine;
using Zenject;

public class BallController : GameElement
{
    private BallView _ballView;

    [Inject]
    public void Setup(BallView ballView)
    {
        _ballView = ballView;
    }
    
    /**
     * <summary>
     * takes a number of a level and generates the balls for the level
     * </summary>
     */
    public void CreateBalls(int levelNumber)
    {
        switch (levelNumber)
        {
            case 0:
                AddBall(BallSize.Medium, new Vector3(0,3.54f,0), 2);
                break;
            
            case 1:
                AddBall(BallSize.Large, new Vector3(0,3.54f,0), 2);
                AddBall(BallSize.Small, new Vector3(2,3.54f,0), 2);
                AddBall(BallSize.Small, new Vector3(5,2.54f,0), -2);
                break;
            
            case 2:
                AddBall(BallSize.Large, new Vector3(0,2.54f,0), 5);
                AddBall(BallSize.Large, new Vector3(5,3.54f,0), -3);
                AddBall(BallSize.Large, new Vector3(2,3.54f,0), 1);
                break;
        }
        UpdateBallModels();
    }

    /**
     * <summary>
     * updates the balls' data accordingly to the ball views
     * </summary>
     */
    public void UpdateBallModels()
    {
        Game.GameModel.BallModels.Clear();
        foreach (var ballView in Game.GameView.BallViews)
        {
            var ballModel = new BallModel(ballView.gameObject.GetInstanceID(),
                (BallSize)(int)(ballView.transform.localScale.x / 0.5f));
            Game.GameModel.BallModels.Add(ballModel);
        }
    }

    /**
     * takes a pre-bisection size of a ball and returns the post-bisection size
     */
    public BallSize GetBallSizeAfterBisection(BallSize ballSize)
    {
        switch (ballSize)
        {
            case BallSize.Large:
                return BallSize.Medium;

            case BallSize.Medium:
                return BallSize.Small;

            case BallSize.Small:
            case BallSize.None:
            default:
                return BallSize.None;
        }
    }

    /**
     * takes a ball-view's id
     * finds and returns the instance of the ball-model corresponding to the ball-view by comparing their ids
     */
    public BallModel GetBallModelById(int instanceId)
    {
        return Game.GameModel.BallModels.Find(ball => ball.Id == instanceId);
    }


    /**
     * <summary>adds a ball to the view</summary>
     * <param name="ballSize">the size of the ball to be added to the view</param>
     * <param name="position">where to position the new ball</param>
     * <param name="initialImpulse">optionally, the user can give the ball an initial impulse to make it fly leftward or rightward</param>
     */
    public void AddBall(BallSize ballSize, Vector3 position, int initialImpulse = 0)
    {
        var newBall = Instantiate(_ballView, position, Quaternion.identity, Game.GameView.transform);
        newBall.transform.localScale *= (int)ballSize;
        if (initialImpulse > 0)
        {
            newBall.GetComponent<Rigidbody>().AddForce(new Vector3(initialImpulse, 0, 0), ForceMode.Impulse);
        }

        Game.GameView.BallViews.Add(newBall);
    }

    /**
     * <summary>removes a ball from view</summary>
     * <param name="ball">the ball to remove from view</param>
     */
    public void RemoveBall(BallView ball)
    {
        Game.GameView.BallViews.Remove(ball);
        Destroy(ball.gameObject);
    }

    /**
     * <summary>removes all balls from view</summary>
     */
    public void RemoveAllBalls()
    {
        foreach (var ballView in Game.GameView.BallViews)
        {
            RemoveBall(ballView);
        }
    }
    
    public void ProcessCommand(CommandType commandType, params object[] data)
    {
        switch (commandType)
        {
            case CommandType.Bisect:
                var ballToBisect = data[0] as GameObject;
                var currentBallSize = GetBallModelById(ballToBisect.GetInstanceID()).BallSize;
                var ballSizeAfterBisection = GetBallSizeAfterBisection(currentBallSize);
                var bisectPosition = ballToBisect.transform.position;

                if (ballSizeAfterBisection != BallSize.None)
                {
                    AddBall(ballSizeAfterBisection, bisectPosition, 4);
                    AddBall(ballSizeAfterBisection, bisectPosition, -4);   
                }

                RemoveBall(ballToBisect.GetComponent<BallView>());
                
                UpdateBallModels();

                Game.GameModel.PlayerModel.Score +=
                    PangConstants.ScoreCoefficient * (Enum.GetNames(typeof(BallSize)).Length - (int)currentBallSize);
                
                Game.GameView.StatsView.UpdateScore();
                
                break;
        }
    }
}
