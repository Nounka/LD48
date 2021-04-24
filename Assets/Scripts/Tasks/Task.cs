using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{

    public enum TaskType
    {
        move,
        attackM,
        attackR,
        build,
        goInside,
        repair,
        gather,
        drop
    }

    public enum TaskBlockage
    {
        doable,
        lackRessources,
        toFar,
        notAvailable,
        itemNeeded
    }

    public TaskType type;
    public WorldEntities actor;
    public Tool activeTool;
    public float taskSpeed;
    public float taskTimer;

    public float taskDistance;

    public virtual TaskBlockage TaskDoable()
    {
        return TaskBlockage.doable;
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

    }
}
