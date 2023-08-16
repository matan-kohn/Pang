using UnityEngine;
using Zenject;

public class Installer : MonoInstaller<Installer>
{
    //Models
    [SerializeField] private GameModel _gameModel;
    [SerializeField] private UIModel _uiModel;
    [SerializeField] private PlayerModel _playerModel;
    
    //Views
    [SerializeField] private GameView _gameView;
    [SerializeField] private ControlsView _controlsView;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private StatsView _statsView;
    [SerializeField] private BulletView _bulletView;
    [SerializeField] private BallView _ballView;
    
    //Controllers
    [SerializeField] private GameController _gameController;
    [SerializeField] private GeneralController _generalController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private BallController _ballController;
    [SerializeField] private BulletController _bulletController;

    public override void InstallBindings()
    {

        //bind the models
        Container.Bind<GameModel>().FromInstance(_gameModel);
        Container.Bind<UIModel>().FromInstance(_uiModel);
        Container.Bind<PlayerModel>().FromInstance(_playerModel);
        
        
        //bind view objects
        Container.Bind<GameView>().FromInstance(_gameView);
        Container.Bind<ControlsView>().FromInstance(_controlsView);
        Container.Bind<PlayerView>().FromInstance(_playerView);
        Container.Bind<StatsView>().FromInstance(_statsView);
        Container.Bind<BulletView>().FromInstance(_bulletView);
        Container.Bind<BallView>().FromInstance(_ballView);

        //bind controller objects
        Container.Bind<GameController>().FromInstance(_gameController);
        Container.Bind<GeneralController>().FromInstance(_generalController);
        Container.Bind<PlayerController>().FromInstance(_playerController);
        Container.Bind<BallController>().FromInstance(_ballController);
        Container.Bind<BulletController>().FromInstance(_bulletController);
    }
}
