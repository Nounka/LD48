using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemDrop : MonoBehaviour
{
    public List<Tool> tools;
    public ResourceStack ressources;
    public List<Schematic> schematics;

    public List<Image> sprites;

    public void AddItems(List<Tool> _tools)
    {
        for(int x = 0; x < _tools.Count; x++)
        {
            tools.Add(_tools[x]);
        }
        
    }

    public void AddItems(List<Schematic> _schematics)
    {
        for(int x = 0; x < _schematics.Count; x++)
        {
            schematics.Add(_schematics[x]);
        }
    }

    public void AddItems(ResourceStack _resources)
    {
        ressources.Add(_resources);
    }

    public ItemDrop()
    {
        tools = new List<Tool>();
        ressources = new ResourceStack(0, 0, 0);
        schematics = new List<Schematic>();
    }

    [System.Serializable]
    public struct Image
    {
        public string name;
        public Sprite sprite;
    }
}
