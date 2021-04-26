using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenGenerator : MonoBehaviour
{
    public GameObject citizenPrefab;

    public Transform citizenRoot;

    public string FindName()
    {
        return "Jean-Pierre";
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
            return retour;
        }
        else
        {
            Debug.Log("Citizen Impropely spawned");
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
