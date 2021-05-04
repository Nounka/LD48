using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorIndicator : MonoBehaviour
{
    public WorldObject follow;
    public SpriteRenderer spriteRenderer;

    public void Select(WorldObject _follow)
    {
        follow = _follow;
        spriteRenderer.enabled = true;
    }

    public void UnSelect()
    {
        follow = null;
        spriteRenderer.enabled = false;
    }

    public void Follow()
    {
        if(follow != null)
        {
            Building build = follow as Building;

            if (build != null)
            {
                if (build.patron.big)
                {
                    transform.localScale = new Vector3(3.5f, 3.5f, 1);
                    transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y + 1.2f, 0);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y + 0.2f, 0);
                }

            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y + 0.2f, 0);
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
        Follow();
    }
}
