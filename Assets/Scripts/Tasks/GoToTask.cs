using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public float Distance(Vector2Int _posa, Vector2Int _posb)
    {
        return Mathf.Abs(_posa.x - _posb.x) + Mathf.Abs(_posa.y - _posb.y);
    }

    public List<Vector2Int> CleanOutside(List<Vector2Int> _target)
    {
        List<Vector2Int> retour = new List<Vector2Int>();

        foreach (Vector2Int pos in _target)
        {
            if (GameState.instance.map.isInMap(pos))
            {
                retour.Add(pos);
            }
        }
        return retour;
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
        List<Vector2Int> closeList = ClosePosition();
        if (closeList.IndexOf(actor.position) != -1)
        {
            // ready to work !
            taskTimer += Time.deltaTime * TaskRatio();
            DoMainTask();
            if (taskTimer > taskSpeed)
            {
                DoTask();
            }
            return;
        }
        taskTimer = 0;
        // Path in progress
        if (secondaryTask != null && secondaryTask.pathToFollow != null)
        {
            if (closeList.IndexOf(secondaryTask.pathToFollow.GetTarget().relatedTile.position) == -1)
            {
                // Target has moved
                secondaryTask = null;
            }
            else
            {
                secondaryTask.WorkTask();
                return;
            }
        }

        Map map = GameState.instance.map;
        // Filter and order possible positions
        Vector2Int[] listPosition = ClosePosition()
                    .Where((position) => map.isInMap(position) && !map.GetTile(position).isBlocking)
                    .OrderBy((position) => Distance(actor.position, position))
                    .ToArray();
        bool isReachable = false;
        Tile actorTile = map.GetTile(actor.position);
        Tile targetTile;
        for (int ind = 0; ind < listPosition.Length; ind++)
        {
            targetTile = map.GetTile(listPosition[ind]);
            Path path = map.GetPath(actorTile, targetTile);
            if (path == null)
            {
                // Unreachable !
            }
            else
            {
                secondaryTask = new MoveTask(path);
                secondaryTask.actor = actor;
                isReachable = true;
                break;
            }
        }
        if (!isReachable)
        {
            CancelTask(TaskBlockage.noPath);
        }
    }
    public virtual void DoMainTask()
    {

    }

    public override void DoTask()
    {
        base.DoTask();
    }
}
