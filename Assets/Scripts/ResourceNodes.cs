using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodes : WorldStaticObject
{
    public ResourceType type;
    public int quantityLeft;
    public AudioBank.AudioName audioGather;

    public ToolType requiredTool;

    public SpriteRenderer berry;
    public BoxCollider colliderNode;

    public override void Destroy()
    {
        if (berry != null)
        {
            berry.enabled = false;
            colliderNode.enabled = false;
        }
        else
        {
            Map map = GameState.instance.map;
            map.GetTile(position.x, position.y).isBlocking = false;
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (position.x != Mathf.FloorToInt(transform.position.x))
        {
            position.x = Mathf.FloorToInt(transform.position.x);
        }
        if (position.y != Mathf.FloorToInt(transform.position.y))
        {
            position.y = Mathf.FloorToInt(transform.position.y);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
