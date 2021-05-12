using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    none,
    lance,
    hache,
    pioche,
    arc,
    EMP
};
[System.Serializable]
public class Tool 
{
    public ToolStats stats;
    /*
    public string displayname;
    public ToolType type;

    // bonus list
    public float damagePerSec;
    public float range; // for combat
    public int carryCapacity;
    //public ResourceStack gatherRate; // resource/sec
    //public float buildingConstructionSpeedMultiplier;
    public int force;
    public float speedModifier;
    // creation
    public ResourceStack cost;
    public float creationTime;*/

    public Tool()
    {

    }
    public Tool(ToolType _type, float _speed, float _dommage, string _name)
    {
        stats = new ToolStats();
        stats.type = _type;
        stats.speedModifier = _speed;
        stats.damagePerSec = _dommage;
        stats.displayName = _name;
    }
      
    public Tool(ToolStats _stats)
    {
        stats = _stats;
    }
}
