using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    public Map map;
    public ResourceStack ressources;
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
        ressources = new ResourceStack(20, 20, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
