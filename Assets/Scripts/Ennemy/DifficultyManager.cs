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

    public AudioMixer mixer;

    public int maxEnnemy;

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
            if (!GameState.instance.overMind.isActive)
            {
                GameState.instance.overMind.isActive = true;
                mixer.SwitchTo(AudioMixer.MusicState.combat);
            }

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
                if(GameState.instance.overMind.minions.Count < maxEnnemy)
                {
                    SpawnOneWave();
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (GameState.instance.overMind.minions.Count < maxEnnemy)
            {
                SpawnOneWave();
            }
            GameState.instance.overMind.isActive = true;
        }
    }
}
