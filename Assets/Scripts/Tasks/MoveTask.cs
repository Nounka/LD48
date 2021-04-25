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
        if (dir.magnitude < Time.deltaTime * actor.baseSpeed)
        {
            actor.transform.position = obj;
            actor.position.x =Mathf.FloorToInt(obj.x);
            actor.position.y = Mathf.FloorToInt(obj.x);
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
