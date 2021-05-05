using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenGenerator : MonoBehaviour
{
    public GameObject citizenPrefab;

    public Transform citizenRoot;

    public List<string> possibleName;
    public List<string> possibleSurname;

    public string FindName()
    {
        int randName = Random.Range(0, possibleName.Count - 1);
        int randSurname = Random.Range(0, possibleSurname.Count - 1);
        string retour = possibleSurname[randSurname] + " " +possibleName[randName];
        return "Jean Pierre";
        return retour; 

    }

    public Citizen CreateCitizen(Vector2Int _position)
    {
        GameObject obj = Instantiate(citizenPrefab,new Vector3(_position.x+0.5f,_position.y,0),Quaternion.identity,citizenRoot);
        Citizen retour = obj.GetComponent<Citizen>();

        if (retour != null)
        {
            retour.position = _position;
            retour.maxCarry = GameState.instance.carryCapacity;
            retour.spriteRend = retour.GetComponent<SpriteRenderer>();
            retour.maxCarry = GameState.instance.carryCapacity;
            retour.nom = FindName();
            retour.currentTool = null;
            GameState.instance.citizens.Add(retour);
            return retour;
        }
        else
        {
            return null;
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
