using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    public Map map;
    public CitizenGenerator citizenGenerator;
    public ItemDrop itemDrop;
    public TaskManager taskManager;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
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
