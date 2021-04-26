using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public int nombreSpawn;

    public float spawnSpeed;
    public float spawnTimer;

    public SpawnEnnemy spawner;

    public void SpawnOneWave()
    {
        List<Ennemy> ajout = spawner.SpawnXEnnemy(nombreSpawn);


        GameState.instance.overMind.GetBackToBase(ajout);

        foreach(Ennemy en in ajout)
        {
            GameState.instance.overMind.minions.Add(en);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnSpeed)
        {
            spawnTimer = 0;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnOneWave();
        }
    }
}
