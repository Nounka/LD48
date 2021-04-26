using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UnitHandlerEnum
{
    building,
    worker,
    enemy,
}

public class UnitStats : MonoBehaviour
{
    public UnitHandlerEnum handler;
    private string GetName()
    {
        switch (handler)
        {
            case UnitHandlerEnum.enemy:
            case UnitHandlerEnum.worker:
                return (GameState.instance.controller.selected as WorldEntities).nom;
            case UnitHandlerEnum.building:
                return (GameState.instance.controller.selected as Building).patron.displayName;
            default:
                return "404 : Not Found";
        }
    }
    private float GetHitpoint() {
        switch(handler) {
            case UnitHandlerEnum.enemy:
            case UnitHandlerEnum.worker:
                return (GameState.instance.controller.selected as WorldEntities).healthCurrent;
            case UnitHandlerEnum.building:
                return (GameState.instance.controller.selected as Building).structurePointCurrent;
            default:
                return 1;
        }
    }
    private float GetHitpointMax()
    {
        switch (handler)
        {
            case UnitHandlerEnum.enemy:
            case UnitHandlerEnum.worker:
                return (GameState.instance.controller.selected as WorldEntities).healthMax;
            case UnitHandlerEnum.building:
                return (GameState.instance.controller.selected as Building).patron.structureFinal;
            default:
                return 1;
        }
    }
    private float GetHitpointBar()
    {
        return GetHitpoint()/GetHitpointMax();
    }

    private Text name, hp;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentsInChildren<Slider>()[0];
        name = GetComponentsInChildren<Text>()[0];
        hp = GetComponentsInChildren<Text>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        float pv = GetHitpoint();
        float pvMax = GetHitpointMax();

        name.text = GetName();
        hp.text = $"{((int)pv).ToString()} / {((int)pvMax).ToString()}";
        slider.value = pv/pvMax;
    }
}
