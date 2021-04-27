using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMixer : MonoBehaviour
{
    public enum MusicState
    {
        calme,
        combat,
        menu
    }

    public AudioClip calme;
    public AudioClip combat;
    public AudioClip menu;

    public AudioSource source;


    public void SwitchTo(MusicState _state)
    {
        switch (_state)
        {
            case (MusicState.calme):
                source.clip = calme;
                break;
            case (MusicState.combat):
                source.clip = combat;
                break;
           case (MusicState.menu):
                source.clip = menu;
                break;
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
