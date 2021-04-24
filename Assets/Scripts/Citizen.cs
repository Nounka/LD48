using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : WorldEntities
{
    public Building insideBuilding;
    public SpriteRenderer spriteRend;


    public Role role;
    public Status status;

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

    public void UpdateState()
    {
        if (state.actions.Count>0)
        {

        }
        else
        {
            if (status.isCivil)
            {
                status.idle = true;
            }
        }
    }

    public void Engage()
    {
        status.isCivil = false;
        status.idle = false;
    }
    public void DisEngage()
    {
        status.isCivil = true;
    }
    public class Role
    {
        public enum RoleType
        {
            transporteur,
            caravanier,
            bucheron,
            fermier,
            mineur
        }
    }
    public class Status
    {
        public bool idle;
        public bool isCivil;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }
}
