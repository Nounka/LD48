using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherTask : GoToTask
{
    public ResourceNodes nodeTarget;

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

    public override Vector2Int ChooseDestination(List<Vector2Int> _possibility)
    {
        Vector2Int retour = new Vector2Int(-1, -1);
        float currentDistance = 0f;

        /*if (_possibility.Contains(destination))
        {
            return destination;
        }*/

        if (_possibility.Count > 0)
        {
            foreach (Vector2Int possi in _possibility)
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

    public override void DoMainTask()
    {
        actor.PlaySound(nodeTarget.audioGather);
    }

    public override void DoTask()
    {
        if (nodeTarget.quantityLeft > 0 && actor.carrying.GetSize() < actor.maxCarry)
        {
            taskTimer -= taskSpeed;
        }
        else
        {
            actor.RemoveTask(this, TaskBlockage.done);
        }
        
        int qt = 1; 
        if (requiredTool != ToolType.none)
        {
            qt = activeTool.stats.force;
        }
        actor.AddRessources(nodeTarget.Harvest(qt));
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
        else if (requiredTool != ToolType.none)
        {
            if(activeTool.stats.type != requiredTool)
            {
                return TaskBlockage.itemNeeded;
            }
            else
            {
                return TaskBlockage.doable;
            }
        }
        else { 
            return TaskBlockage.doable;
        }

    }

    public override float TaskRatio()
    {
        if (requiredTool != ToolType.none)
        {
            if (activeTool.stats.type != requiredTool)
            {
                return 0f;
            }
            else
            {
                return activeTool.stats.speedModifier;
            }
        }
        else
        {
            return 1f;
        }
    }

    public override void CancelTask(TaskBlockage _status)
    {
        if(_status== TaskBlockage.itemNeeded)
        {
            actor.RemoveTask(this, _status);
        }
        else{
            actor.RemoveTask(this,_status);
        }
    }

    public override bool IsRole(Citizen.Role _role)
    {
        return base.IsRole(_role);
    }

    public GatherTask(ResourceNodes _target,WorldEntities _actor)
    {
        nodeTarget = _target;
        actor = _actor;
        taskSpeed = GameState.instance.gatherSpeed;
        requiredTool = _target.requiredTool;
        type = TaskType.gather;
        unavailablePosition = new List<Vector2Int>();
    }
}
