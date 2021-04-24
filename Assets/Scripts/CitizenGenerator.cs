using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenGenerator : MonoBehaviour
{
    public GameObject citizenPrefab;

    public string FindName()
    {
        return "Jean-Pierre";
    }

    public Citizen CreateCitizen()
    {
        GameObject obj = Instantiate(citizenPrefab);
        Citizen retour = obj.GetComponent<Citizen>();

        if (retour != null)
        {
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
