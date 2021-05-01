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
        return Mathf.Abs(_posa.x - _posb.x) + Mathf.Abs(_posa.y - _posb.y);
    }

    public List<Vector2Int> CleanOutside(List<Vector2Int> _target){
        List<Vector2Int> retour = new List<Vector2Int>();
        
        foreach(Vector2Int pos in _target)
        {
            bool ajout = true;
            if (pos.x < 0 || pos.x > GameState.instance.map.width)
            {
                ajout = false;
            }
            if (pos.y < 0 || pos.y > GameState.instance.map.length)
            {
                ajout = false;
            }
            if (ajout)
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
    public List<Vector2Int> unavailablePosition;
    public override void WorkTask()
    {
        List<Vector2Int> listPosition;
        listPosition = ClosePosition();
        foreach(Vector2Int vect in unavailablePosition)
        {
            if (listPosition.Contains(vect))
            {
                listPosition.Remove(vect);
            }
        }
        listPosition = CleanOutside(listPosition);
        if (listPosition.Count > 0)
        {
            destination = ChooseDestination(listPosition);
            TaskBlockage status = TaskDoable();

            switch (status)
            {
                
                case (TaskBlockage.doable):
                    if (destination != actor.position)
                    {
                        Map map = GameState.instance.map;
                        if (secondaryTask != null)
                        {
                            if(secondaryTask.pathToFollow != null)
                            {
                                if (secondaryTask.pathToFollow.waypoints[0] != null)
                                {
                                    if (secondaryTask.pathToFollow.waypoints[0].relatedTile.position != destination)
                                    {
                                        secondaryTask = new MoveTask(map.GetPath(map.GetTile(actor.position.x, actor.position.y), map.GetTile(destination.x, destination.y)));
                                        secondaryTask.actor = actor;

                                    }
                                    else
                                    {
                                        /*secondaryTask = new MoveTask(map.GetPath(map.GetTile(actor.position.x, actor.position.y), map.GetTile(destination.x, destination.y)));
                                        secondaryTask.actor = actor;*/
                                    }
                                }
                                else
                                {

                                    unavailablePosition.Add(destination);
                                    WorkTask();
                                }
                            }
                            else
                            {
                                secondaryTask.pathToFollow = map.GetPath(map.GetTile(actor.position.x, actor.position.y), map.GetTile(destination.x, destination.y));
                                if (secondaryTask.pathToFollow == null)
                                {
                                    unavailablePosition.Add(destination);
                                    WorkTask();
                                }

                            }

                            secondaryTask.WorkTask();
                            taskTimer = 0;
                        }
                        else
                        {
                            secondaryTask = new MoveTask(map.GetPath(map.GetTile(actor.position.x, actor.position.y), map.GetTile(destination.x, destination.y)));
                            secondaryTask.actor = actor;

                        }


                    }
                    else
                    {
                        taskTimer += Time.deltaTime * TaskRatio();

                        DoMainTask();
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
        else
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

    public override bool IsRole(Citizen.Role _role)
    {
        return base.IsRole(_role);
    }
}
