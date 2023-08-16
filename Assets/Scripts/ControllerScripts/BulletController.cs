using DefaultNamespace;
using UnityEngine;
using Zenject;

public class BulletController : GameElement, IController
{
    private BulletView _bulletView;

    [Inject]
    public void Setup(BulletView bulletView)
    {
        _bulletView = bulletView;
    }

    /**
    * <summary>generates an instance of a bullet when the player clicked on the 'shoot' button</summary>
    */
    public void AddBullet()
    {
        var newBullet = Instantiate(_bulletView, Game.GameView.PlayerView.transform.position, Quaternion.identity);
        Game.GameView.BulletViews.Add(newBullet);
        newBullet.GetComponent<Rigidbody>().AddForce(Vector3.up * 20f, ForceMode.VelocityChange);
    }

    /**
    * <summary>removes all the bullets from view</summary>
    */
    public void RemoveAllBullets()
    {
        for (var i = 0; i < Game.GameView.BulletViews.Count; i++)
        {
            var bulletView = Game.GameView.BulletViews[i];
            Game.GameView.BulletViews.Remove(bulletView);
            Destroy(bulletView.gameObject);
        }
    }


    public void ProcessCommand(CommandType commandType, params object[] data)
    {
        var playerAnimator = Game.GameView.PlayerView.GetComponent<Animator>();
        playerAnimator.ResetTrigger(PangConstants.GoLeftTrigger);
        playerAnimator.ResetTrigger(PangConstants.GoRightTrigger);
        playerAnimator.ResetTrigger(PangConstants.ShootTrigger);
        playerAnimator.ResetTrigger(PangConstants.IdleTrigger);

        switch (commandType)
        {
            case CommandType.Shoot:
                playerAnimator.SetTrigger(PangConstants.ShootTrigger);
                AddBullet();
                break;
        }
    }
}
