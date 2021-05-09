using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPack
{
    public ToolStats stats;
    public int quantity;
}
public class ProductionRuin 
{
    public List<ToolPack> requiredTools;
    public int requireCitizen;
    public ResourceStack requiredResource;
    
    public float speed;

    public List<Schematic> unlockReward;
    public List<ToolPack> toolReward;
    public ResourceStack resourceReward;
 
}
[CreateAssetMenu(fileName = "Ruin", menuName = "ScriptableObjects/Ruin", order = 1)]
public class RuinStats : ScriptableObject
{

    public class Chance
    {

    }
}
public class Ruin : WorldStaticObject
{
    public List<Tool> toolsAvailable;
    public ResourceStack ressourcesAvailable;
    public List<Citizen> workingInside;

    public List<ProductionRuin> availableInRuin;
    public ProductionRuin currentProduction;

    public float currentProgression;

    public Vector2Int dropPosition;

    public List<Tool> GetToType(ToolPack _compare)
    {
        List<Tool> retour = new List<Tool>();

        for(int x = 0; x < toolsAvailable.Count; x++)
        {
            if (toolsAvailable[x].stats == _compare.stats)
            {
                retour.Add(toolsAvailable[x]);
            }
        }
        return retour;
    }

    public float GetRatioProgression()
    {
        float retour = 1f;
        if (currentProduction.requireCitizen > 0)
        {
            retour = workingInside.Count / currentProduction.requireCitizen;

        }
        foreach (ToolPack tools in currentProduction.requiredTools)
        {
            int toolsAvailable = GetToType(tools).Count;
            float multiply = toolsAvailable / currentProduction.requiredTools.Count;
            retour *= multiply;
        }

        retour *= ressourcesAvailable.RatioBrut(currentProduction.requiredResource);

        return retour;
    }

    public void Produce()
    {
        ItemDropManager dropManager = GameState.instance.itemDropManager;
        dropManager.AddResources(currentProduction.resourceReward,dropPosition);
        List<Tool> ajoutTool = new List<Tool>();
        foreach(ToolPack pack in currentProduction.toolReward)
        {
            for (int x = 0; x < pack.quantity; x++)
            {
                ajoutTool.Add(new Tool(pack.stats));
            }
        }
        dropManager.AddTools(ajoutTool, dropPosition);



        dropManager.AddSchematics(currentProduction.unlockReward, dropPosition);

    }

    public void SwitchProduction()
    {

    }
    public void Progress()
    {
        float ratio = GetRatioProgression();
        currentProgression += ratio * Time.deltaTime;

        if (currentProgression >= currentProduction.speed)
        {
            availableInRuin.Remove(currentProduction);
            Produce();
            SwitchProduction();
            currentProgression = 0;
        }
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
