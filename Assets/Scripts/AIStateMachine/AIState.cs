using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AIState
{
    protected WorldEntities owner;

    public AIState(WorldEntities _owner)
    {
        owner = _owner;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Execute();
    public abstract AIState DoTransition();
    public abstract string GetStateNameDebugStr();
}
