using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeProductionUI : MonoBehaviour
{
    public GameObject noneProd;
    public GameObject prodPrefab;

    public EventSystem system;

    public void SetAllProduction()
    {

    }

    public void ResizeViewPort(int _number)
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(system.IsPointerOverGameObject());
    }
}
