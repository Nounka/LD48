using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockTask : Task
{
    Building target;

    public override void DoTask(WorldEntities entity)
    {
        target.Stock(actor.carrying);
        actor.carrying.woodCount = 0;
        actor.carrying.foodCount = 0;
        actor.carrying.stoneCount = 0;
        entity.EndCurrentTask();
    }

    public override TaskBlockage TaskDoable()
    {
        if (target != null)
        {
            return TaskBlockage.doable;
        }
        else
        {
            return TaskBlockage.notAvailable;
        }
    }

    public StockTask(Building _target)
    {
        target = _target;
        position = _target.position;
        taskDistance = Mathf.Max(_target.size.x, _target.size.y);
        taskSpeed = GameState.instance.dropSpeed;
    }
}
