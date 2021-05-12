using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    public IdleState(WorldEntities _owner) : base(_owner)
    {
    }

    public override AIState DoTransition()
    {
        if(owner.currentTask == null)
        {

        }

        if(owner.currentTask != null)
        {
            return new WorkState(owner);
        }

        return null;
    }

    public override void Enter()
    {

    }

    public override void Execute()
    {

    }

    public override void Exit()
    {

    }

    public override string GetStateNameDebugStr()
    {
        return "Idle";
    }
}
