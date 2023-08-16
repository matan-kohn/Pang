using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BallModel
{
    public int Id { get; }
    public BallSize BallSize { get; }

    public BallModel(int id, BallSize ballSize)
    {
        Id = id;
        BallSize = ballSize;
    }
}
