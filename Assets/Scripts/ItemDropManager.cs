using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{

    public GameObject itemDropPrefab;

    public ItemDrop CreateItemDrop(Vector2Int _position)
    {
        GameObject obj = Instantiate(itemDropPrefab);
        ItemDrop retour = obj.GetComponent<ItemDrop>();

        if(retour != null)
        {
            retour.transform.position = new Vector3(_position.x + 0.5f, _position.y, 0);
            GameState.instance.map.GetTile(_position.x, _position.y).items = retour;
            return retour;
        }
        else
        {
            return null;
        }
    }

    public void AddResources(ResourceStack _resource,Vector2Int _position)
    {
        if (_resource.GetSize()>0)
        {
            ItemDrop item = GameState.instance.map.GetTile(_position.x, _position.y).items;
            if (item != null)
            {
                item.AddItems(_resource);
            }
            else
            {
                ItemDrop drop = CreateItemDrop(_position);
                drop.AddItems(_resource);
            }
        }

    }

    public void AddSchematics(List<Schematic> _schematics,Vector2Int _position)
    {
        if (_schematics.Count > 0)
        {
            ItemDrop item = GameState.instance.map.GetTile(_position.x, _position.y).items;
            if (item != null)
            {
                item.AddItems(_schematics);
            }
            else
            {
                ItemDrop drop = CreateItemDrop(_position);
                drop.AddItems(_schematics);
            }
        }

    }

    public void AddTools(List<Tool> _tools, Vector2Int _position)
    {
        if (_tools.Count > 0)
        {
            ItemDrop item = GameState.instance.map.GetTile(_position.x, _position.y).items;
            if (item != null)
            {
                item.AddItems(_tools);
            }
            else
            {
                ItemDrop drop = CreateItemDrop(_position);
                drop.AddItems(_tools);
            }
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
