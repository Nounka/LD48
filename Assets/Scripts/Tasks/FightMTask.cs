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

        foreach(Vector2Int voisine in GameState.neighboursVectorD)
        {
            retour.Add(new Vector2Int(target.position.x + voisine.x, target.position.y + voisine.y));
        }
        return retour;
    }

    public override Vector2Int ChooseDestination(List<Vector2Int> _possibility)
    {
        Vector2Int retour = new Vector2Int(-1, -1);
        float currentDistance = 0f;
        if (_possibility.Count > 0)
        {
            foreach(Vector2Int possi in _possibility)
            {
                if (possi.x >= 0 && possi.x < GameState.instance.map.width)
                {
                    if (possi.y >= 0 && possi.y < GameState.instance.map.length)
                    {
                        if (retour.x == -1)
                        {
                            retour = possi;
                            currentDistance = Distance(actor.position, possi);
                        }
                        float test = Distance(actor.position, possi);
                        if (test < currentDistance)
                        {
                            retour = possi;
                            currentDistance = Distance(actor.position, possi);
                        }
                        
                    }
                }
            }
            
            
        }
        return retour;
    }



    public override TaskBlockage TaskDoable()
    {
        if (target == null)
        {
            return TaskBlockage.notAvailable;
        }
        else if(destination.x<0)
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
            entitieTarget.TakeDommage(GameState.instance.allDommage,actor);
            if (entitieTarget.healthCurrent < 0)
            {
                actor.RemoveTask(this, TaskBlockage.done);
            }
        }
        else if(building!=null)
        {
            building.TakeDommage(activeTool.stats.damagePerSec);
            if (building.structurePointCurrent < 0)
            {
                actor.RemoveTask(this, TaskBlockage.done);
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
        else {
            actor.PlaySound(AudioBank.AudioName.robotFight);
                }
    }

    public override void CancelTask(TaskBlockage _status)
    {
        base.CancelTask(_status);
    }

    public override bool IsRole(Citizen.Role _role)
    {
        return true;
    }
    public FightMTask(WorldEntities _actor,WorldObject _target)
    {
        target = _target;
        actor = _actor;
        activeTool = _actor.GetTool();
        taskSpeed = GameState.instance.combatSpeed;
        unavailablePosition = new List<Vector2Int>();
    }
}
