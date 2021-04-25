using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPack
{
    public ToolStats stats;
    public int quantity;
}
public class ProductionRuin : Production
{
    public List<ToolPack> require;

    public List<Schematic> canUnlock;
    public List<ToolPack> canGet;
    public List<ResourceStack> canObtain;

 
}
[CreateAssetMenu(fileName = "Ruin", menuName = "ScriptableObjects/Ruin", order = 1)]
public class RuinStats : ScriptableObject
{

    public class Chance
    {

    }
}
public class Ruin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
