using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName = "ScriptableObjects/ToolStats", order = 1)]
public class ToolStats : ScriptableObject
{
    public ToolType type;
    public string displayName;

    public float damagePerSec;
    public float attackSpeed;
    public int force;
    public float speedModifier;
    public ResourceStack cost;
    public float creationTime;
    public Sprite sprite;
    public ResourceType ressourceType;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
