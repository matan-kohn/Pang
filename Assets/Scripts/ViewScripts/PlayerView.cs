using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerView : GameElement
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(PangTags.Ball))
        {
            Game.GameController.ProcessCommand(CommandType.ApplyDamage, collision.collider.gameObject);
        }
    }
}
