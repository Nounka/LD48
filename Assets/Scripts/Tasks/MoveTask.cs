using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MoveTask : Task
{
    public Path pathToFollow;

    public MoveTask(Path _pathToFollow)
    {
        pathToFollow = _pathToFollow;
    }
    Vector3 obj = new Vector3(-100,0,0);
    public override void WorkTask()
    {
        if (obj.x < -10) {
            Waypoint way = pathToFollow.waypoints[pathToFollow.waypoints.Count - 1];
            obj.x = way.relatedTile.position.x+0.5f;
            obj.y = way.relatedTile.position.y;
        }


        obj.z = 0;
        Vector3 dir = (obj - actor.transform.position);
        Waypoint current = pathToFollow.waypoints[pathToFollow.waypoints.Count - 1];
        Vector2Int moveDirection = new Vector2Int();
        if (current.origin!=null)
        {
            moveDirection.x = current.relatedTile.position.x - current.origin.relatedTile.position.x;
            moveDirection.y = current.relatedTile.position.y - current.origin.relatedTile.position.y;

        }

        if (moveDirection.x == 1)
        {
            actor.SetAnimatorState(false, false, false, true);//GoLEft
        }else if (moveDirection.x == -1)
        {
            actor.SetAnimatorState(false, false, true, false);//GoRight
        }
        if (moveDirection.y == -1)
        {
            actor.SetAnimatorState(false, true, false, false);//Go Down
        }
        else if(moveDirection.y==1)
        {
            actor.SetAnimatorState(true, false, false, false);//Go Up
        }/*
        if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y)){
            if (moveDirection.x > 0)
            {
                actor.SetAnimatorState(false, false, true, false);//GoRight
            }
            else if(moveDirection.x<0)
            {
                
            }
            else
            {
                if (moveDirection.y > 0)
                {
                    actor.SetAnimatorState(true, false, false, false);//Go Up
                }
                else
                {
                    actor.SetAnimatorState(false, true, false, false);//Go Down
                }

            }
        }
        else
        {
            if (moveDirection.y > 0)
            {
                actor.SetAnimatorState(true, false, false, false);//Go Up
            }
            else if(moveDirection.y<0)
            {
                actor.SetAnimatorState(false, true, false, false);//Go Down
            }
            else
            {
                if (moveDirection.x > 0)
                {
                    actor.SetAnimatorState(false, false, true, false);//GoRight
                }
                else
                {
                    actor.SetAnimatorState(false, false, false, true);//GoLEft
                }
            }
        }*/
        if (dir.magnitude < Time.deltaTime * actor.baseSpeed)
        {
            actor.transform.position = obj;
            actor.position.x =Mathf.FloorToInt(obj.x);
            actor.position.y = Mathf.FloorToInt(obj.y);
            pathToFollow.RemoveLast();
            if (pathToFollow.waypoints.Count > 0)
            {

                Waypoint next = pathToFollow.GetNextPoint();
                obj.x = next.relatedTile.position.x+0.5f;
                obj.y = next.relatedTile.position.y;
            }
            else
            {
                obj.x = -1000;
                actor.SetAnimatorState(false, false, false, false);
                DoTask();
            }
        }
        else 
        {
            actor.transform.position = actor.transform.position + (dir.normalized * Time.deltaTime * actor.baseSpeed);
        }

    }

    public override void DoTask()
    {
        actor.RemoveTask(this, TaskBlockage.done);
    }

}
