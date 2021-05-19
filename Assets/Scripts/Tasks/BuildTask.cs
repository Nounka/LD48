using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTask : Task
{
    public Building construction;

    public override TaskBlockage TaskDoable() {
        if (construction!=null)
        {
            if(construction.isConstructing)
            {
                return TaskBlockage.doable;
            }
            else
            {
                return TaskBlockage.notAvailable;
            }         
        }
        else
        {
            return TaskBlockage.notAvailable;
        }
            
    }

    public override void WorkTask(WorldEntities entity)
    {
        base.WorkTask(entity);
    }

    public override void DoTask(WorldEntities entity)
    {
        construction.WorkOnBuilding(GetWorkValue());
        if (construction.isConstructing)
        {
            taskTimer = 0;
        }
        else
        {
            entity.EndCurrentTask();
        }
    }

    public float GetWorkValue()
    {
        return 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        taskDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override float TaskRatio()
    {
        return 1f;
    }

    public BuildTask(Building _target)
    {
        construction = _target;
        taskSpeed = GameState.instance.buildSpeed;
        taskDistance = Mathf.Max(_target.size.x, _target.size.y);
        type = TaskType.build;
        position = _target.position;
    }
}
