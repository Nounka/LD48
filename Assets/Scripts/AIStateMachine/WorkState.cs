using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkState : AIState
{
    public WorkState(WorldEntities _owner) : base(_owner)
    {
    }

    public override AIState DoTransition()
    {
        throw new System.NotImplementedException();
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override string GetStateNameDebugStr()
    {
        return "Work";
    }
}