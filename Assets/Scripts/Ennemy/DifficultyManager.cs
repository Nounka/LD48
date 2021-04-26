using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public int nombreSpawn;

    public float spawnSpeed;
    public float spawnTimer;

    public float time;
    public float robotStartAssault;

    public float goUpTimer;
    public float goUpSpeed;

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

        time += Time.deltaTime;
        if (time > robotStartAssault)
        {
            GameState.instance.overMind.isActive = true;
        }
        if (GameState.instance.overMind.isActive)
        {
            spawnTimer += Time.deltaTime;
            goUpTimer += Time.deltaTime;
            if (goUpTimer > goUpSpeed)
            {
                goUpTimer = 0;
                GameState.instance.overMind.agressionHauteur++;
            }
            if (spawnTimer > spawnSpeed)
            {
                spawnTimer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnOneWave();
        }
    }
}
