using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTask : GoToTask
{
    public Building construction;



    public override TaskBlockage TaskDoable() {
        return TaskBlockage.doable;
    }

    public override void WorkTask()
    {
        base.WorkTask();
    }
    public override void DoTask()
    {
        construction.construction.workCurrent += GetWorkValue();
    }

    public float GetWorkValue()
    {
        return 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override List<Vector2Int> ClosePosition()
    {
        return base.ClosePosition();
    }

    public override Vector2Int ChooseDestination(List<Vector2Int> _possibility)
    {
        return base.ChooseDestination(_possibility);
    }

    public override void DoMainTask()
    {
        actor.PlaySound(AudioBank.AudioName.construction);
    }
}
