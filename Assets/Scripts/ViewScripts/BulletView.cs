using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BulletView : GameElement
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PangTags.Ball))
        {
            Game.GameController.ProcessCommand(CommandType.Bisect, other.gameObject);
            Game.GameView.BulletViews.Remove(this);
            Destroy(gameObject);
        }
    }
}
