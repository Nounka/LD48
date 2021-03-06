using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Schematic
{
    public Production production;
    public Building.BuildingType type;
}
public class SchematicUnlock : MonoBehaviour
{
    public List<Schematic> unlocks;
    public List<Schematic> unlockable;

    public List<Production> GetProductionAvailable(Building.BuildingType _type)
    {
        List<Production> retour = new List<Production>();
        foreach(Schematic schem in unlocks)
        {
            if (schem.type == _type)
            {
                retour.Add(schem.production);
            }
        }
        return retour;
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
