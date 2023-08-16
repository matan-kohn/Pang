using UnityEngine;
using Zenject;

public class Installer : MonoInstaller<Installer>
{

    [SerializeField] private BulletView _bulletView;
    [SerializeField] private BallView _ballView;
    
    public override void InstallBindings()
    {
        Container.Bind<BulletView>().FromInstance(_bulletView);
        Container.Bind<BallView>().FromInstance(_ballView);
    }
}
