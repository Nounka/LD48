using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemy : MonoBehaviour
{
    public GameObject ennemyPrefab;
    public Transform ennemyRoot;


    public List <Ennemy> SpawnXEnnemy(int _number)
    {
        List<Vector2Int> spawnPossible= new List<Vector2Int>();
        List<Ennemy> ajout = new List<Ennemy>();
        Vector2Int enBase = GameState.instance.overMind.BasePlace;
        spawnPossible.Add(new Vector2Int(enBase.x+1,0));
        spawnPossible.Add(new Vector2Int(enBase.x+2,0));
        spawnPossible.Add(new Vector2Int(enBase.x-1,0));
        spawnPossible.Add(new Vector2Int(enBase.x-2,0));
        spawnPossible.Add(new Vector2Int(enBase.x,0));
        for(int x = 0; x < _number; x++)
        {
            int rand = Random.Range(0, spawnPossible.Count-1);
            GameObject obj = Instantiate(ennemyPrefab, new Vector3(spawnPossible[x].x + 0.5f, spawnPossible[x].y, 0), Quaternion.identity, ennemyRoot);
            Ennemy en = obj.GetComponent<Ennemy>();
            if (en != null)
            {
                en.position = new Vector2Int(spawnPossible[x].x, spawnPossible[x].y);
                ajout.Add(en);

            }
        }
        return ajout;
        
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
