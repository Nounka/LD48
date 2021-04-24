using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{

    public enum TaskType
    {
        move,
        attackM,
        attackR,
        build,
        goInside,
        repair,
        gather
    }

    public enum TaskBlockage
    {
        doable,
        lackRessources,
        toFar,
        notAvailable
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
        if (TaskDoable()==TaskBlockage.doable)
        {
            taskTimer += TaskRatio() * Time.deltaTime;
        }

        if (taskTimer > taskSpeed)
        {
            DoTask();
        }
    }

    public virtual void DoTask()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
