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
        if(Vector2Int.Distance(owner.currentTask.GetPosition(), owner.position) > owner.currentTask.taskDistance)
        {
            return new MoveState(owner);
        }

        return null;
    }

    public override void Enter()
    {

    }

    public override void Execute()
    {
        owner.currentTask.WorkTask(owner);
    }

    public override void Exit()
    {

    }

    public override string GetStateNameDebugStr()
    {
        return "Work";
    }
}
