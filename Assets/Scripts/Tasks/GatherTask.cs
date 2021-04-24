using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherTask : Task
{
    public ResourceNodes nodeTarget;

    public override void DoTask()
    {
        if (nodeTarget.quantityLeft > activeTool.force)
        {
            actor.AddRessources(new ResourceStack(nodeTarget.type,activeTool.force));
            actor.
        }
        else
        {
            actor.AddRessources(new ResourceStack(nodeTarget.type, nodeTarget.quantityLeft));
        }
        
    }

    public override TaskBlockage TaskDoable()
    {
        if (nodeTarget == null)
        {
            return TaskBlockage.notAvailable;
        }
        else if (activeTool.type == requiredTool)
        {
            return TaskBlockage.itemNeeded;
        }
        else { 
            return TaskBlockage.doable;
        }

    }

    public override float TaskRatio()
    {
        if (activeTool.type != requiredTool)
        {
            return 0f;
        }
        else
        {
            return activeTool.speedModifier;
        }
    }

    public override void WorkTask()
    {
        base.WorkTask();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
