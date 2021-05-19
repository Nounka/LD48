using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMTask : Task
{
    public WorldObject target;
    public bool isDefensive;

    public override TaskBlockage TaskDoable()
    {
        if (target == null)
        {
            return TaskBlockage.notAvailable;
        }
        else
        {
            return TaskBlockage.doable;
        }
    }

    public override float TaskRatio()
    {
        return base.TaskRatio();
    }

  

    public override void DoTask(WorldEntities entity)
    {
        WorldEntities entitieTarget = target.GetComponent<WorldEntities>();
        Building building = target.GetComponent<Building>();
        WorldStaticObject staticObject = target.GetComponent<WorldStaticObject>();

        if (entitieTarget != null)
        {
            entitieTarget.TakeDommage(GameState.instance.allDommage,actor);
            if (entitieTarget.healthCurrent < 0)
            {
                entity.EndCurrentTask();
            }
        }
        else if(building!=null)
        {
            building.TakeDommage(actor.GetTool().stats.damagePerSec);
            if (building.structurePointCurrent < 0)
            {
                entity.EndCurrentTask();
            }
        }
        
        else 
        {
        }

    }

    public override bool IsRole(Citizen.Role _role)
    {
        return true;
    }

    public FightMTask(WorldObject _target)
    {
        target = _target;
        taskSpeed = GameState.instance.combatSpeed;
        taskDistance = 1.0f;
        position = _target.position;
    }

    void Update()
    {
        position = target.position;
    }
}
