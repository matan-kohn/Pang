using DefaultNamespace;
using UnityEngine;

public class BulletView : GameElement
{
    /**
     * <summary>detects and handles collisions between bullets and other game-objects (i.e. balls)</summary>
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PangTags.Ball))
        {
            Game.GameController.BallController.ProcessCommand(CommandType.Bisect, other.gameObject);
            Game.GameView.BulletViews.Remove(this);
            Destroy(gameObject);
        }
    }
}
