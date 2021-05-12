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

    public void SetSlider(int _productionDone,int _productionSpeed)
    {
        duration.text = _productionDone.ToString()+"/"+ _productionSpeed.ToString();
        slider.value = _productionDone / _productionSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
