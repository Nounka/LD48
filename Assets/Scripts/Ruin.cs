using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionRuin : Production
{
    public List<ToolPack> require;

    public List<Schematic> canUnlock;
    public List<ToolPack> canGet;
    public List<ResourceStack> canObtain;

   public class ToolPack
    {
        public ToolStats stats;
        public int quantity;
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
