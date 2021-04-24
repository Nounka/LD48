using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : WorldEntities
{
    public Building insideBuilding;
    public SpriteRenderer spriteRend;

    public void MoveTo(Vector2 _position)
    {
        transform.position = new Vector3(_position.x, position.y, 0);
    }

    public void GetInside()
    {
        spriteRend.enabled = false;
    }
    public void GetOutside()
    {
        spriteRend.enabled = true;
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
