using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject toolBoxPrefab;
    public GameObject ressourcesPrefab;

    public ToolBox CreateToolBox(List<Tool> _tools,Vector2Int _position)
    {
        GameObject obj = Instantiate(toolBoxPrefab);
        ToolBox retour = obj.GetComponent<ToolBox>();

        if (retour != null)
        {
            retour.tools = _tools;
            GameState.instance.map.GetTile(_position.x, _position.y).relatedObject = retour;
            return retour;
        }
        else
        {
            return null;
        }
    }

    public ResourceDrop CreateRessourceStack(ResourceStack _stack,Vector2Int _position)
    {
        GameObject obj = Instantiate(ressourcesPrefab);
        ResourceDrop drop = obj.GetComponent<ResourceDrop>();
        if(drop != null)
        {
            drop.ressources = _stack;
            GameState.instance.map.GetTile(_position.x, _position.y).relatedObject = drop;
            return drop;
        }
        else
        {
            return null;
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
