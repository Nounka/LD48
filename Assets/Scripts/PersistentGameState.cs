using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameState : MonoBehaviour
{
    public static PersistentGameState instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Options
    public float audioVolume = 0.5f;

}
