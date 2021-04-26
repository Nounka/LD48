using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverMind : MonoBehaviour
{
    public List<Ennemy> minions;
    public List<Objectif> objectifs;

    public int agressionHauteur;

    public Objectif CreateObjectif(List<Ennemy> _assigned)
    {
        RefreshObjectif();
        List<Citizen> target = GameState.instance.GetCitizenBellow(agressionHauteur);
    
        foreach(Objectif objectif in objectifs)
        {
            if(target.Contains(objectif.target as Citizen))
            {

            }
        }
        return null;
    }


    public class Objectif
    {
        public Vector2Int position;

        public WorldObject target;

        public List<Ennemy> assigned;

        public float objectifStrength;

        public void CheckCondition()
        {
            if (target != null)
            {
                if (target.position != position)
                {
                    position = target.position;
                }
                Citizen cit = target as Citizen;

                if (cit != null)
                {
                    if (cit.insideBuilding != null)
                    {
                        target = cit.insideBuilding;
                    }
                }
            }
            else
            {

            }
        }
        public void CancelObjectif()
        {

        }

    }

    public void RefreshObjectif()
    {
        foreach(Objectif objectif in objectifs)
        {
            objectif.CheckCondition();
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
