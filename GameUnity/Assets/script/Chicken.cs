using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChickenState
{
    idleState,
    moveState
}

public class Chicken : Enemy
{
    private ChickenState chickenState;

    protected override void Start()
    {
        base.Start();

        chickenState = ChickenState.idleState;
    }
}
