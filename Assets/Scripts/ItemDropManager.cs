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


    public void AddTools(List<Tool> _tools, Vector2Int _position)
    {
        ItemDrop item = GameState.instance.map.GetTile(_position.x, _position.y).items;
        if (item!=null){
            item.AddItems(_tools);
        }
        else
        {
            ItemDrop drop = CreateItemDrop(_position);
            drop.AddItems(_tools);       
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
