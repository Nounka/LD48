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

    public void CheckSound()
    {
        try
        {
            source.volume = PersistentGameState.instance.audioVolume;
        }
        catch (System.Exception e)
        {
            // Do nothing
            //source.volume = 1;
        }
    }
    public void SwitchTo(MusicState _state)
    {
        switch (_state)
        {
            case (MusicState.calme):
                source.clip = calme;
                source.Play();
                break;
            case (MusicState.combat):
                source.clip = combat;
                source.Play();
                break;
           case (MusicState.menu):
                source.clip = menu;
                source.Play();
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
        CheckSound();
    }
}
