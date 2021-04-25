using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static bool isGamePaused = false;
    public static double dirx, diry;

    private const float cameraSizeMin = 5f;
    private const float cameraSizeMax = 20f;
    private const double cameraSpeed = 20f;
    private Camera _camera;
    private Map map;

    // Start is called before the first frame update
    void Start()
    {
        dirx = 0;
        diry = 0;
        map = GameState.instance.map;
        _camera = GetComponent<Camera>();
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

        double speed = Time.deltaTime / cameraSizeMin * _camera.orthographicSize * cameraSpeed;
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

        float cameraSize = _camera.orthographicSize;
        _camera.orthographicSize = (float)(cameraSize - Input.mouseScrollDelta.y * speed);
        if (_camera.orthographicSize < cameraSizeMin)
        {
            _camera.orthographicSize = cameraSizeMin; // Min size 
        }
        if (_camera.orthographicSize > cameraSizeMax)
        {
            _camera.orthographicSize = cameraSizeMax; // Max size
        }

        cameraSize = _camera.orthographicSize;
        float camx = (float)(transform.position.x + dirx);
        float camy = (float)(transform.position.y + diry);

        if ( camx < cameraSize) {
            dirx = cameraSize - transform.position.x;
        }
        if ( camx > map.width - cameraSize) {
            dirx = (map.width - cameraSize) - transform.position.x;
        }
        if (camy < cameraSize)
        {
            diry = cameraSize - transform.position.y;
        }
        if (camy > map.length - cameraSize)
        {
            diry = (map.length - cameraSize) - transform.position.y;
        }

        transform.Translate(new Vector3((float)dirx,(float) diry, 0));
    }
}
