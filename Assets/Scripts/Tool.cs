using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    lance,
    hache,
    pioche,
    arc
};

public class Tool : MonoBehaviour
{
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
    public float creationTime;



}
