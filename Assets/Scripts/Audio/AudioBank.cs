using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBank : MonoBehaviour
{
    public enum AudioName
    {
        marche,
        mine,
        meurt,
        fight,

    }
    public AudioClip marche;
    public AudioClip mine;
    public AudioClip meurt;
    public AudioClip combat;
    public AudioClip buildingCrumble;
    public AudioClip robotMove;
    public AudioClip robotFight;

    public AudioClip GetSound()
    {
        return marche;
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
