using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : WorldEntities
{
    public Building insideBuilding;
    public SpriteRenderer spriteRend;
    public Role role;


    public void MoveTo(Vector2 _position)
    {
        transform.position = new Vector3(_position.x, position.y, 0);
    }

    public void GetInside(Building _building)
    {
        spriteRend.enabled = false;
        insideBuilding = _building;
    }
    public void GetOutside()
    {
        spriteRend.enabled = true;
    }

    public void UpdateState()
    {
        
    }

    public void Engage()
    {

    }

    public void DisEngage()
    {

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

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        position.x = (int) transform.position.x;
        position.y = (int) transform.position.y;
        taskManager = GameState.instance.citizenTaskManager;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
        UpdateState();
    }
}
