using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameController : GameElement
{
    private Animator _playerAnimator;
    private BallView _ballView;
    private BulletView _bulletView;

    [Inject]
    public void Setup(BallView ballView, BulletView bulletView)
    {
        _ballView = ballView;
        _bulletView = bulletView;
    }
    
    void Start()
    {
        _playerAnimator = Game.GameView.PlayerView.GetComponent<Animator>();
        StartCoroutine(GameLoop());
    }


    private IEnumerator GameLoop()
    {
        var currentLevel = Game.GameModel.UIModel.Level;
        var numberOfLevels = Game.GameModel.UIModel.NumberOfLevels;
        CreateBalls(currentLevel);
        while (currentLevel < numberOfLevels && Game.GameModel.PlayerModel.Lives > 0)
        {
            if (Game.GameView.BallViews.Count == 0)
            {
                RemoveAllBalls();
                RemoveAllBullets();
                currentLevel = Game.GameModel.UIModel.LevelUp();
                CreateBalls(currentLevel);
                Game.GameView.StatsView.UpdateLevel();
            }

            yield return null;
        }

        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }


    private void CreateBalls(int levelNumber)
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

    public const string GoLeftTrigger = "RunLeft";
    public const string GoRightTrigger = "RunRight";
    public const string ShootTrigger = "Shoot";
    public const string IdleTrigger = "Idle";

    public void ProcessCommand(CommandType commandType, params object[] data)
    {
        _playerAnimator.ResetTrigger(GoLeftTrigger);
        _playerAnimator.ResetTrigger(GoRightTrigger);
        _playerAnimator.ResetTrigger(ShootTrigger);
        _playerAnimator.ResetTrigger(IdleTrigger);

        switch (commandType)
        {
            case CommandType.GoLeft:
                _playerAnimator.SetTrigger(GoLeftTrigger);
                StartCoroutine(MovePlayerLeft());
                break;
            
            case CommandType.GoRight:
                _playerAnimator.SetTrigger(GoRightTrigger);
                StartCoroutine(MovePlayerRight());
                break;
            
            case CommandType.Shoot:
                _playerAnimator.SetTrigger(ShootTrigger);
                AddBullet();
                break;
            
            case CommandType.Stand:
                _playerAnimator.SetTrigger(IdleTrigger);
                Game.GameModel.PlayerModel.PlayerState = PlayerState.Idle;
                break;
            
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
                    10 * (Enum.GetNames(typeof(BallSize)).Length - (int)currentBallSize);
                
                Game.GameView.StatsView.UpdateScore();
                
                break;
            
            case CommandType.ApplyDamage:
                Game.GameModel.PlayerModel.Lives--;
                Game.GameView.StatsView.UpdateLives();
                break;
            
            case CommandType.Restart:
                Destroy(GameObject.FindWithTag(PangTags.ProjectContext));
                SceneManager.LoadScene("Menu");
                break;
        }
    }

    private void UpdateBallModels()
    {
        Game.GameModel.BallModels.Clear();
        foreach (var ballView in Game.GameView.BallViews)
        {
            var ballModel = new BallModel(ballView.gameObject.GetInstanceID(),
                (BallSize)(int)(ballView.transform.localScale.x / 0.5f));
            Game.GameModel.BallModels.Add(ballModel);
        }
    }

    private BallSize GetBallSizeAfterBisection(BallSize ballSize)
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

    public BallModel GetBallModelById(int instanceId)
    {
        return Game.GameModel.BallModels.Find(ball => ball.Id == instanceId);
    }

    private void AddBall(BallSize ballSize, Vector3 position, int initialImpulse = 0)
    {
        var newBall = Instantiate(_ballView, position, Quaternion.identity, Game.GameView.transform);
        newBall.transform.localScale *= (int)ballSize;
        if (initialImpulse > 0)
        {
            newBall.GetComponent<Rigidbody>().AddForce(new Vector3(initialImpulse, 0, 0), ForceMode.Impulse);
        }
        Game.GameView.BallViews.Add(newBall);
    }

    private void RemoveBall(BallView ball)
    {
        Game.GameView.BallViews.Remove(ball);
        Destroy(ball.gameObject);
    }

    private void RemoveAllBalls()
    {
        foreach (var ballView in Game.GameView.BallViews)
        {
            RemoveBall(ballView);
        }
    }

    private void AddBullet()
    {
        var newBullet = Instantiate(_bulletView, Game.GameView.PlayerView.transform.position, Quaternion.identity);
        Game.GameView.BulletViews.Add(newBullet);
        newBullet.GetComponent<Rigidbody>().AddForce(Vector3.up * 20f, ForceMode.VelocityChange);
    }

    private void RemoveAllBullets()
    {
        for(var i = 0; i < Game.GameView.BulletViews.Count; i++)
        {
            var bulletView = Game.GameView.BulletViews[i];
            Game.GameView.BulletViews.Remove(bulletView);   
            Destroy(bulletView.gameObject);
        }
    }

    private IEnumerator MovePlayerLeft()
    {
        Game.GameModel.PlayerModel.PlayerState = PlayerState.GoingLeft;
        Game.GameView.PlayerView.transform.rotation = Quaternion.Euler(0,0,0);
        while (Game.GameModel.PlayerModel.PlayerState == PlayerState.GoingLeft)
        {
            Game.GameView.PlayerView.transform.position += Vector3.left * (5f * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MovePlayerRight()
    {
        Game.GameModel.PlayerModel.PlayerState = PlayerState.GoingRight;
        Game.GameView.PlayerView.transform.rotation = Quaternion.Euler(0,180,0);
        while (Game.GameModel.PlayerModel.PlayerState == PlayerState.GoingRight)
        {
            Game.GameView.PlayerView.transform.position += Vector3.right * (5f * Time.deltaTime);
            yield return null;
        }
    }
}
