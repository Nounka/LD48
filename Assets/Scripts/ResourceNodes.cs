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
        Map map = GameState.instance.map;
        map.GetTile(position.x, position.y).isBlocking = false;
        Destroy(gameObject);
    }

    public ResourceStack Harvest(int qt) {
        int quantity = Mathf.Min(quantityLeft,qt);
        quantityLeft -= quantity;
        if (quantityLeft == 0) {
            if (berry) {
                berry.enabled = false;
                colliderNode.enabled = false;
            } else {
                Destroy();
            }
        }
        return new ResourceStack(type, quantity);
    }

    // Start is called before the first frame update
    void Start()
    {
        position.x = Mathf.FloorToInt(transform.position.x);
        position.y = Mathf.FloorToInt(transform.position.y);
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
