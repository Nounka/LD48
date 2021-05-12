using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionOutput : MonoBehaviour
{
    public Text Count;
    public Image Renderer;

    public void SetUp(Production _production)
    {
        Count.text = _production.toolQuantity.ToString();
        Renderer.sprite = _production.tool.sprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        Count.text = "0";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
