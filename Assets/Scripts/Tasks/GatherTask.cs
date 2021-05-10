using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherTask : GoToTask
{
    public ResourceNodes nodeTarget;
    public ResourceGatherStats gatherStats;
    public bool needTool;

    public override List<Vector2Int> ClosePosition()
    {
        List<Vector2Int> retour = new List<Vector2Int>();
        //retour.Add(nodeTarget.position);

        foreach (Vector2Int voisine in GameState.neighboursVectorD)
        {
            retour.Add(new Vector2Int(nodeTarget.position.x + voisine.x, nodeTarget.position.y + voisine.y));
        }
        return retour;
    }

    public override void DoMainTask()
    {
        actor.PlaySound(nodeTarget.audioGather);
    }

    public override void DoTask()
    {
        if (nodeTarget.quantityLeft > 0 && actor.carrying.GetSize() < actor.maxCarry)
        {
            taskTimer -= taskSpeed;
            int qt = gatherStats.quantity;
            actor.AddRessources(nodeTarget.Harvest(qt));
        }
        else
        {
            CancelTask(TaskBlockage.done);
        }
    }

    public override void WorkTask()
    {
        base.WorkTask();
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

    public GatherTask(ResourceNodes _target, WorldEntities _actor)
    {
        nodeTarget = _target;
        actor = _actor;
        if (actor.currentTool != null)
        {
            if(actor.currentTool.stats.ressourceType == _target.type)
            {
                gatherStats = GameState.instance.GetGatherStats(_target, _actor.currentTool);
            }
            else
            {
                gatherStats = GameState.instance.GetGatherStats(_target);
            }
        }
        else
        {
            gatherStats = GameState.instance.GetGatherStats(_target);
        }

        taskSpeed = gatherStats.speed;
        type = TaskType.gather;
    }
}
