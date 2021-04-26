using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    private Text duration;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentsInChildren<Slider>()[0];
        duration = GetComponentsInChildren<Text>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        Building building = (GameState.instance.controller.selected as Building);

        if (building.isActive) {
            duration.text = $"{building.productionDone.ToString()} / {building.productionSpeed.ToString()}";
            slider.value = building.productionDone / building.productionSpeed;
        } else {
            duration.text = "";
            slider.value = 0;
        }
    }
}
