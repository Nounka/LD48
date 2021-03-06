using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{

    public enum TaskType
    {
                
        attackM,// Attribuer par le joueur
        attackR,
        goInside,
        move,//Mix //Sans outils
        get,//Automatic
        drop,
        build,//Besoin Outils
        repair,
        gather,
        none
    }

    public enum TaskBlockage
    {
        doable,
        lackRessources,
        toFar,
        notAvailable,
        itemNeeded,
        done,
        noPath
    }

    public TaskType type;
    public WorldEntities actor;
    public ToolType requiredTool;

    public float taskSpeed;
    public float taskTimer;
    public Vector2Int position;

    public float taskDistance;

    public virtual TaskBlockage TaskDoable()
    {
        return TaskBlockage.notAvailable;
    }

    public virtual float TaskRatio()
    {
        return 1f;
    }

    public virtual void WorkTask()
    {
        TaskBlockage status = TaskDoable();
        if (status==TaskBlockage.doable)
        {
            taskTimer += TaskRatio() * Time.deltaTime;
        }
        else {
            CancelTask(status);
        }

        if (taskTimer > taskSpeed)
        {
            DoTask();
        }
    }

    public virtual void DoTask()
    {

    }

    public virtual void CancelTask(TaskBlockage _status)
    {
        actor.RemoveTask(this, _status);
    }
    public virtual bool IsRole(Citizen.Role _role)
    {
        return false;
    }

}
