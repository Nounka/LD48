using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTask : Task//Des taches qui demande d'allez a une position pour etre effectuer
{

    public Vector2Int destination;

    public MoveTask secondaryTask;


    public virtual List<Vector2Int> ClosePosition()
    {
        List<Vector2Int> retour = new List<Vector2Int>();
        return retour;
    }

    public virtual Vector2Int ChooseDestination(List<Vector2Int> _possibility)
    {
        Vector2Int retour = new Vector2Int(-1, -1);
        return retour;
    }




    public float Distance(Vector2Int _posa, Vector2Int _posb)
    {
        return Mathf.Abs(_posa.x - _posb.y) + Mathf.Abs(_posa.y - _posb.y);
    }



    public override TaskBlockage TaskDoable()
    {
        return base.TaskDoable();
    }

    public override float TaskRatio()
    {
        return base.TaskRatio();
    }

    public override void WorkTask()
    {
        TaskBlockage status = TaskDoable();
        switch (status)
        {
            case (TaskBlockage.doable):
                if (destination != actor.position)
                {
                    Map map = GameState.instance.map;
                    if (secondaryTask != null)
                    {
                        if (secondaryTask.pathToFollow.waypoints[0].relatedTile.position != destination)
                        {
                            secondaryTask = new MoveTask(map.GetPath(map.GetTile(position.x, position.y), map.GetTile(destination.x, destination.y)));
                            secondaryTask.actor = actor;
                        }
                    }
                    else
                    {
                        secondaryTask = new MoveTask(map.GetPath(map.GetTile(position.x, position.y), map.GetTile(destination.x, destination.y)));
                        secondaryTask.actor = actor;
                    }
                    secondaryTask.WorkTask();
                    taskTimer = 0;
                }
                else
                {
                    taskTimer += Time.deltaTime * TaskRatio();

                    if (taskTimer > taskSpeed)
                    {
                        DoTask();
                    }
                }
                break;
            default:
                CancelTask(status);
                break;

        }
    }

    public override void DoTask()
    {
        base.DoTask();
    }

    public override void CancelTask(TaskBlockage _status)
    {
        actor.RemoveTask(this, _status);
    }

    public override bool IsRole(Citizen.Role _role)
    {
        return base.IsRole(_role);
    }
}
