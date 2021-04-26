using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInsideBuilding : GoToTask
{
    public Building building;

    public override List<Vector2Int> ClosePosition()
    {
        List<Vector2Int> retour = new List<Vector2Int>();
        retour.Add(building.entrance);
        return retour;
    }

    public override Vector2Int ChooseDestination(List<Vector2Int> _possibility)
    {
        return building.entrance;
    }

    public override void DoTask()
    {
        Citizen cit = (Citizen)actor;
        if (building.TryGetInside(cit))
        {
            cit.GetInside(building);
            if (GameState.instance.controller.selected == cit)
            {
                GameState.instance.controller.UnSelect();
            }
            actor.RemoveTask(this, TaskBlockage.done);

        }
        else
        {
            actor.RemoveTask(this, TaskBlockage.notAvailable);
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

    public GoInsideBuilding(Building __building,Citizen _actor)
    {
        building = __building;
        actor = _actor;
        unavailablePosition = new List<Vector2Int>();
        taskTimer = 1;
    }
}
