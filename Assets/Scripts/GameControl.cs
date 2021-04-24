using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static bool isGamePaused = false; 
    public static double dirx, diry;

    private const double cameraSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        dirx = 0;
        diry = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Pause))
        {
            GameControl.isGamePaused = !GameControl.isGamePaused;
            Time.timeScale = 1 - (GameControl.isGamePaused?1:0);
        }

        // User input are avoided when game is paused
        if (GameControl.isGamePaused) {
            return;
        }

        double speed = Time.deltaTime * cameraSpeed;
        dirx = 0;
        diry = 0;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            diry -= speed;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
        {
            diry += speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q))
        {
            dirx -= speed;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            dirx += speed;
        }
        transform.Translate(new Vector3((float)dirx,(float) diry, 0));
    }
}
