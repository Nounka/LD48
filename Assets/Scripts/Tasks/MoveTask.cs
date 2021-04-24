using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTask : Task
{
    public Path pathToFollow;

    public MoveTask(Path _pathToFollow)
    {
        pathToFollow = _pathToFollow;
    }

    public override void WorkTask()
    {
        Vector3 obj = new Vector3();
        Waypoint next = pathToFollow.GetNextPoint();
        obj.x = next.relatedTile.position.x;
        obj.y = next.relatedTile.position.y;
        obj.z = 0;
        Vector3 dir = (obj - actor.transform.position);
        if (dir.magnitude < Time.deltaTime * actor.baseSpeed)
        {
            actor.transform.position = obj;
            if (!pathToFollow.RemoveLast())
            {
                DoTask();
            }
        }
        else 
        {
            actor.transform.position = actor.transform.position + (dir.normalized * Time.deltaTime * actor.baseSpeed);
        }
    }
}
