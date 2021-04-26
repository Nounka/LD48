using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockTask : GoToTask
{
    Building target;
    public override Vector2Int ChooseDestination(List<Vector2Int> _possibility)
    {
        return _possibility[0];
    }

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
    }

    public override TaskBlockage TaskDoable()
    {
        return base.TaskDoable();
    }

    public StockTask(Building _target,Citizen _actor)
    {
        target = _target;
        actor = _actor;

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
