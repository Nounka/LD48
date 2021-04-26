using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : WorldEntities
{
    public float force;

    public List<Path> patrol;

    public void Patrol()
    {

    }


    void Start()
    {
        this.nom = "Evil Robots";
    }
}
