using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerModel : GameElement
{
    public PlayerState PlayerState { get; set; }
    public int Lives { get; set; }
    public int Score { get; set; }

    private void Awake()
    {
        PlayerState = PlayerState.Idle;
        Lives = 3;
        Score = 0;
    }
}
