using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherTask : Task
{
    public ResourceNodes nodeTarget;
    public ResourceGatherStats gatherStats;
    public bool needTool;

    public override void DoTask(WorldEntities entity)
    {
        if (nodeTarget.quantityLeft > 0 && actor.carrying.GetSize() < actor.maxCarry)
        {
            taskTimer -= taskSpeed;
            int qt = gatherStats.quantity;
            actor.AddRessources(nodeTarget.Harvest(qt));
        }
        else
        {
            entity.EndCurrentTask();
        }
    }

    public override void WorkTask(WorldEntities entity)
    {
        base.WorkTask(entity);
    }

    public override TaskBlockage TaskDoable()
    {
        if (nodeTarget == null)
        {
            return TaskBlockage.notAvailable;
        }
        if (needTool)
        {
            return TaskBlockage.itemNeeded;
        }
        if (gatherStats.quantity == 0)
        {
            return TaskBlockage.itemRequired;
        }
        return TaskBlockage.doable;
    }

    public override float TaskRatio()
    {
        if (requiredTool != ToolType.none)
        {
            if (actor.GetTool().stats.type != requiredTool)
            {
                return 0f;
            }
            else
            {
                return actor.GetTool().stats.speedModifier;
            }
        }
        else
        {
            return 1f;
        }
    }

    public GatherTask(ResourceNodes _target)
    {
        nodeTarget = _target;
        position = _target.position;
        taskDistance = 1.0f;
        taskSpeed = gatherStats.speed;
        type = TaskType.gather;

    }
}
