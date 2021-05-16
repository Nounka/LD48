using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : AIState
{
    public MoveState(WorldEntities _owner) : base(_owner)
    {
    }

    public override AIState DoTransition()
    {
        if (Vector2Int.Distance(owner.currentTask.GetPosition(), owner.position) <= owner.currentTask.taskDistance
            || owner.currentPath == null || owner.currentPath.waypoints.Count == 0)
        {
            return new WorkState(owner);
        }

        return null;
    }

    public override void Enter()
    {
        owner.currentPath = GameState.instance.map.GetPath(owner.position, owner.currentTask.position);
    }

    public override void Execute()
    {
        Vector3 moveDirection = new Vector3();
        Waypoint current = owner.currentPath.GetNextPoint();
        Vector3 obj = current.relatedTile.tileCenterPosition;
        moveDirection = owner.transform.position - current.relatedTile.tileCenterPosition;
        float remainingDistance = moveDirection.magnitude;
        moveDirection /= remainingDistance; // To prevent useless recomputation of magnitude, we don't use the Normalize method

        if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
        {
            if (moveDirection.x > 0)
            {
                owner.SetAnimatorState(true, 1);//GoLeft
            }
            else
            {
                owner.SetAnimatorState(true, 3);//GoRight
            }
        }
        else
        {
            if (moveDirection.y > 0)
            {
                owner.SetAnimatorState(true, 0);//Go Down
            }
            else
            {
                owner.SetAnimatorState(true, 2);//Go Up
            }
        }

        if (owner.isCitizen)
        {
            owner.PlaySound(AudioBank.AudioName.marche);
        }
        else
        {
            owner.PlaySound(AudioBank.AudioName.robotMove);
        }

        if (remainingDistance < Time.deltaTime * owner.baseSpeed)
        {
            owner.transform.position = obj;
            owner.position.x = Mathf.FloorToInt(obj.x);
            owner.position.y = Mathf.FloorToInt(obj.y);
            owner.currentPath.RemoveLast();
        }
        else
        {
            owner.transform.position = owner.transform.position + (moveDirection * Time.deltaTime * owner.baseSpeed);
        }
    }

    public override void Exit()
    {
        owner.currentPath = null;
    }

    public override string GetStateNameDebugStr()
    {
        return "Move";
    }
}
