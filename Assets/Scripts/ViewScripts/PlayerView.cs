using DefaultNamespace;
using UnityEngine;

public class PlayerView : GameElement
{
    /**
     * <summary>detects and handles collisions between the player and other game objects</summary>
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(PangTags.Ball))
        {
            Game.GameController.PlayerController.ProcessCommand(CommandType.ApplyDamage, collision.collider.gameObject);
        }
    }
}
