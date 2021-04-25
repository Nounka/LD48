using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Building", menuName = "ScriptableObjects/Building", order = 1)]
public class BuildingStats : ScriptableObject
{
    public string displayName;
    public string description;
    public float structureConstruction;
    public float structureFinal;

    public bool big;

    public int workerRequired;
    public ResourceStack buildCost;

    public ResourceStack stock;

    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
