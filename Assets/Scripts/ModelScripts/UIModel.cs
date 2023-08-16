using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModel : GameElement
{
    public int Level { get; private set; }

    public int NumberOfLevels => 5;

    private void Awake()
    {
        Level = 0;
    }

    public int LevelUp()
    {
        return ++Level;
    }
}
