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

    public int robotDefend;

    public float extraAgression;

    public bool isActive;
    public Objectif CreateObjectif(List<Ennemy> _assigned, WorldObject _target)
    {
        List<Ennemy> select = SelectForce(iddleMinions);
        Objectif retour = new Objectif(_target, select);
        return retour;
    }

    public Objectif CreateObjectif(List<Ennemy> _assigned)
    {
        RefreshObjectif();
        List<Citizen> target = GameState.instance.GetCitizenBellow(agressionHauteur);
        int randomTarget = Random.Range(0, 2);
        if (randomTarget == 0)
        {

            if (target.Count > 0)
            {
                List<Ennemy> select = SelectForce(iddleMinions);
                if (select.Count > 0)
                {
                    Objectif retour = new Objectif(target[0], select);
                    Debug.Log("ObjCit");
                    return retour;
                }
            }
        }
        else
        {
            List<Building> secondaryTarget = GameState.instance.GetBuildingBellow(agressionHauteur);

            if (secondaryTarget.Count > 0)
            {
                List<Ennemy> select = SelectForce(iddleMinions);
                if (select.Count > 0)
                {
                    Objectif retour = new Objectif(secondaryTarget[0], select);
                    Debug.Log("ObjCBUILD");
                    return retour;
                }
            }
        }
        return null;
    }

    public Objectif GetNewObjectif(Objectif _previous)
    {
        List<Citizen> targets = GameState.instance.citizens;
        Citizen choice = null;
        float distance = float.MaxValue;
        foreach (Citizen target in targets)
        {
            Vector2Int direction = target.position - _previous.position;
            if (direction.magnitude < extraAgression)
            {
                if (direction.magnitude < distance)
                {
                    choice = target;
                    distance = direction.magnitude;
                }
            }
        }
        if (choice != null)
        {
            Objectif retour = new Objectif(choice, _previous.assigned);
            Debug.Log("ObjCit");
            return retour;
        }
        else
        {
            List<Building> buildTargets = GameState.instance.buildingsOnMap;
            Building buildChoice = null;
            distance = float.MaxValue;
            foreach (Building target in buildTargets)
            {
                Vector2Int direction = target.position - _previous.position;
                if (direction.magnitude < extraAgression)
                {
                    if (direction.magnitude < distance)
                    {
                        buildChoice = target;
                        distance = direction.magnitude;
                    }
                }
            }
            if (buildChoice != null)
            {
                Objectif retour = new Objectif(buildChoice, _previous.assigned);
                Debug.Log("Objbuild");
                return retour;
            }
            else
            {
                return null;
            }
        }


    }

    public void GetBackToBase(List<Ennemy> _minions)
    {
        Map map = GameState.instance.map;
        foreach (Ennemy en in _minions)
        {
            en.state.orderedTask = new MoveTask(map.GetPath(map.GetTile(en.position.x, en.position.y), map.GetTile(BasePlace.x, BasePlace.y)));
            en.state.orderedTask.actor = en;
        }
    }

    public List<Ennemy> SelectForce(List<Ennemy> _available)
    {
        List<Ennemy> retour = new List<Ennemy>();
        int nombre = Random.Range(GameState.instance.ennemySize.x, GameState.instance.ennemySize.y);

        if (nombre > _available.Count)
        {
            nombre = _available.Count;
        }
        for (int x = 0; x < nombre; x++)
        {
            retour.Add(_available[x]);
        }
        return retour;
    }
    [System.Serializable]
    public class Objectif
    {
        public Vector2Int position;

        public WorldObject target;

        public List<Ennemy> assigned;

        public float objectifStrength;

        public bool done;

        public bool CheckCondition()
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
                return true;
            }
            else
            {
                Debug.Log("TargetEliminated");
                return false;
            }
        }


        public Objectif(WorldObject _target, List<Ennemy> _minions)
        {
            target = _target;
            position = _target.position;
            assigned = _minions;

        }
    }

    private void clearNull<T> (List<T> list) {
        for (int x = list.Count - 1; x >= 0; x--)
        {
            if (list[x] == null)
            {
                list.RemoveAt(x);
            }
        }
    }

    public void RefreshObjectif()
    {
        List<Objectif> toRemove = new List<Objectif>();
        foreach (Objectif objectif in objectifs)
        {
            clearNull(objectif.assigned);
            if (objectif.assigned.Count > 0)
            {
                if (!objectif.CheckCondition())
                {
                    GetBackToBase(objectif.assigned);
                    toRemove.Add(objectif);
                }
                else
                {
                    // Okay !
                }
            }
            else
            {
                toRemove.Add(objectif);
            }
        }
        foreach (Objectif objectif in toRemove)
        {
            objectifs.Remove(objectif);
        }
    }

    public void CheckRobotIddle()
    {
        foreach (Ennemy en in minions)
        {
            if (!iddleMinions.Contains(en))
            {
                if (en.isPatroling == false)
                {
                    if (en.state.orderedTask == null)
                    {
                        if (en.position == BasePlace)
                        {
                            iddleMinions.Add(en);
                            en.Patrol();
                        }
                    }
                }
            }

        }

    }

    public void DoObjectif(Objectif _obj)
    {
        foreach (Ennemy en in _obj.assigned)
        {
            en.state.orderedTask = new FightMTask(en, _obj.target);
            iddleMinions.Remove(en);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (objectifs == null)
        {
            objectifs = new List<Objectif>();
        }
        if (minions == null)
        {
            minions = new List<Ennemy>();
        }
        if (iddleMinions == null)
        {
            iddleMinions = new List<Ennemy>();
        }
        attackSpeed = GameState.instance.attackSpeed;
    }

    public float checkTimer;
    public float checkTime = 2f;

    public float attackTimer;
    public float attackSpeed;
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackSpeed)
            {
                attackTimer = 0;
                if (iddleMinions.Count > robotDefend)
                {

                    Objectif create = CreateObjectif(SelectForce(iddleMinions));
                    if (create != null)
                    {

                        DoObjectif(create);
                        objectifs.Add(create);
                        Debug.Log("cible:" + create.position);
                    }

                }

            }

            checkTimer += Time.deltaTime;
            if (checkTimer > checkTime)
            {
                CheckRobotIddle();
                RefreshObjectif();
                clearNull(minions);
                clearNull(iddleMinions);
                checkTimer = 0;
            }
        }
    }
}
