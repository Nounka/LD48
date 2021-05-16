using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewButtonSwitch : MonoBehaviour
{
    public bool open;
    public Transform arrow;

    public delegate void SwitchButton(bool _state);
    public SwitchButton switchButton;


    public void Switch()
    {
        if (open)
        {
            arrow.localRotation = Quaternion.Euler(0, 0, 0);
            open = false;
            if (switchButton != null)
            {
                switchButton(open);
            }

        }
        else
        {
            arrow.localRotation = Quaternion.Euler(0, 0, 180);
            open = true;
            if (switchButton != null)
            {
                switchButton(open);
            }
        }
            
    }

    public void SetTo(bool _state)
    {
        open = _state;
        if (!open)
        {
            arrow.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            arrow.localRotation = Quaternion.Euler(0, 0, 180);
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
