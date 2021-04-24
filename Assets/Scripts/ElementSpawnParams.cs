using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ElementSpawnParams : ScriptableObject
{
    public GameObject elementPrefab;

    public float spawnProbabilityEdge;
    public float spawnProbabilityCenter;
}
