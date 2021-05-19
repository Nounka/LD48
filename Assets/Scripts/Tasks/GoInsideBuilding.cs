using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInsideBuilding : Task
{
    public Building building;

    public override void DoTask(WorldEntities entity)
    {
        Citizen cit = (Citizen)actor;
        if (building.TryGetInside(cit))
        {
            cit.GetInside(building);
            if (GameState.instance.controller.selected == cit)
            {
                GameState.instance.controller.UnSelect();
            }
            entity.CancelCurrentTask();

        }
        else
        {
            entity.CancelCurrentTask();
        }

    }

    public override TaskBlockage TaskDoable()
    {
        if (building.GetSpace() < building.workers.Length)
        {
            return TaskBlockage.doable;
        }
        else
        {
            return TaskBlockage.notAvailable;
        }
    }

    public GoInsideBuilding(Building __building)
    {
        building = __building;
        position = __building.position;
        taskDistance = Mathf.Max(__building.position.x, __building.position.y);
        taskTimer = 1;
    }
}
