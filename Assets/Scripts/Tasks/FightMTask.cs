using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMTask : GoToTask
{
    public WorldObject target;
    public bool isDefensive;

    public override List<Vector2Int> ClosePosition()
    {
        List<Vector2Int> retour = new List<Vector2Int>();
        //retour.Add(target.position);

        foreach (Vector2Int voisine in GameState.neighboursVectorD)
        {
            retour.Add(new Vector2Int(target.position.x + voisine.x, target.position.y + voisine.y));
        }
        return retour;
    }

    public override TaskBlockage TaskDoable()
    {
        if (target == null)
        {
            return TaskBlockage.notAvailable;
        }
        else if (destination.x < 0)
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



    public override void DoTask()
    {
        WorldEntities entitieTarget = target.GetComponent<WorldEntities>();
        Building building = target.GetComponent<Building>();
        WorldStaticObject staticObject = target.GetComponent<WorldStaticObject>();

        if (entitieTarget != null)
        {
            entitieTarget.TakeDommage(GameState.instance.allDommage, actor);
            if (entitieTarget.healthCurrent < 0)
            {
                CancelTask(TaskBlockage.done);
            }
        }
        else if (building != null)
        {
            building.TakeDommage(GameState.instance.allDommage);
            if (building.structurePointCurrent < 0)
            {
                CancelTask(TaskBlockage.done);
            }
        }

        else
        {
        }

    }

    public override void DoMainTask()
    {
        if (actor.isCitizen)
        {
            actor.PlaySound(AudioBank.AudioName.fight);
        }
        else
        {
            actor.PlaySound(AudioBank.AudioName.robotFight);
        }
    }

    public FightMTask(WorldEntities _actor, WorldObject _target)
    {
        target = _target;
        actor = _actor;
        taskSpeed = GameState.instance.combatSpeed;
    }
}
