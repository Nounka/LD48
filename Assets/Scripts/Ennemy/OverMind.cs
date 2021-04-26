using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverMind : MonoBehaviour
{
    public List<Ennemy> minions;
    public List<Ennemy> iddleMinions;

    public List<Objectif> objectifs;

    public Vector2Int BasePlace;

    public int agressionHauteur;

    public Objectif CreateObjectif(List<Ennemy> _assigned)
    {
        RefreshObjectif();
        List<Citizen> target = GameState.instance.GetCitizenBellow(agressionHauteur);
    
        foreach(Objectif objectif in objectifs)
        {
            if(target.Contains(objectif.target as Citizen))
            {
                target.Remove(objectif.target as Citizen);
            }
        }
        if (target.Count > 0)
        {
            List<Ennemy> select = SelectForce(iddleMinions);
            if (select.Count > 0)
            {
                Objectif retour = new Objectif(target[0], select);
                return retour;
            }
        }
        else
        {
            List<Building> secondaryTarget = GameState.instance.GetBuildingBellow(agressionHauteur);

            foreach(Objectif objectif in objectifs)
            {
                if (secondaryTarget.Contains(objectif.target as Building))
                {
                    secondaryTarget.Remove(objectif.target as Building);
                }
            }
            if (secondaryTarget.Count > 0)
            {
                List<Ennemy> select = SelectForce(iddleMinions);
                if (select.Count > 0)
                {
                    Objectif retour = new Objectif(secondaryTarget[0], select);
                    return retour;
                }
            }
        }
        return null;
    }

    public List<Ennemy> SelectForce(List<Ennemy> _available)
    {
        List<Ennemy> retour = new List<Ennemy>();
        int nombre = Random.Range(GameState.instance.ennemySize.x, GameState.instance.ennemySize.y);

        if (nombre > _available.Count)
        {
            nombre = _available.Count;
        }
        for(int x = 0; x < nombre; x++)
        {
            retour.Add(_available[x]);
        }
        return retour;
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

        public Objectif(WorldObject _target,List<Ennemy> _minions)
        {
            target = _target;
            position = _target.position;
            assigned = _minions;

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
