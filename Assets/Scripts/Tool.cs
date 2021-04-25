using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    none,
    lance,
    hache,
    pioche,
    arc
};

public class Tool : MonoBehaviour
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



}
[CreateAssetMenu(fileName = "Tool", menuName = "ScriptableObjects/ToolStats", order = 1)]
public class ToolStats : ScriptableObject
{
    public ToolType type;
    public string displayName;

    public float damagePerSec;
    public int force;
    public float speedModifier;
    public ResourceStack cost;
    public float creationTime;
    public Sprite sprite;
}
