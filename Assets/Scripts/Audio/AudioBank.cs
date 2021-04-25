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
        plante,
        buildingCrumble,
        robotMove,
        robotFight,
        construction,
        wood

    }
    public AudioClip marche;
    public AudioClip mine;
    public AudioClip meurt;
    public AudioClip combat;
    public AudioClip buildingCrumble;
    public AudioClip robotMove;
    public AudioClip robotFight;
    public AudioClip construction;
    public AudioClip coupePlante;
    public AudioClip sawWood;

    public AudioClip GetSound(AudioName _name)
    {
        switch (_name)
        {
            case (AudioName.marche):
                return marche;
                break;
            case (AudioName.mine):
                return mine;
                break;
            case (AudioName.meurt):
                return meurt;
                break;
            case (AudioName.fight):
                return combat;
                break;
            case (AudioName.plante):
                return coupePlante;
                break;
            case (AudioName.buildingCrumble):
                return buildingCrumble;
                break;
            case (AudioName.robotMove):
                return robotFight;
                break;
            case (AudioName.robotFight):
                return robotMove;
                break;
            case (AudioName.construction):
                return construction;
            case (AudioName.wood):
                return sawWood;
            default:
                return combat;
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
