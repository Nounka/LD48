using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockTask : GoToTask
{
    Building target;

    public override List<Vector2Int> ClosePosition()
    {
        List<Vector2Int> retour = new List<Vector2Int>();
        retour.Add(target.entrance);
        return retour;
    }

    public override void DoMainTask()
    {
        actor.PlaySound(AudioBank.AudioName.stockRessource);
    }

    public override void DoTask()
    {
        target.Stock(actor.carrying);
        actor.carrying.woodCount = 0;
        actor.carrying.foodCount = 0;
        actor.carrying.stoneCount = 0;
        CancelTask( TaskBlockage.done);
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

    public StockTask(Building _target,Citizen _actor)
    {
        target = _target;
        actor = _actor;
        taskSpeed = GameState.instance.dropSpeed;
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
