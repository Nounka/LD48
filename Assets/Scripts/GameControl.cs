using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStateEnum
{
    inTuto,
    inGame,
    inGameDefeat,
    inGameVictory,
}


public class GameControl : MonoBehaviour
{
    public static bool isGamePaused = false;
    public static double dirx, diry;
    public static float cameraDistance = 1f;

    private const float cameraSizeMin = 4f;
    private const float cameraSizeMax = 12f;
    private const double cameraSpeed = 20f;
    private Camera _camera;
    private Map map;

    public GameObject InfoPanel;
    private Text InfoTitle;
    public Text InfoContent;


    public static void setPause(bool state)
    {
        isGamePaused = state;
        Time.timeScale = 1 - (GameControl.isGamePaused ? 1 : 0);
    }

    public static GameStateEnum state = GameStateEnum.inTuto;

    // Start is called before the first frame update
    void Start()
    {
        dirx = 0;
        diry = 0;
        map = GameState.instance.map;
        _camera = GetComponent<Camera>();

        InfoTitle = InfoPanel.GetComponentsInChildren<Text>()[0];
        InfoContent = InfoPanel.GetComponentsInChildren<Text>()[1];
        setPause(true);
    }

    public string[] tutorialScreens;
    private int selectedTuto = 0;
    void HandleTuto()
    {
        InfoPanel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            selectedTuto--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedTuto++;
        }
        
        if (selectedTuto < 0)
        {
            selectedTuto = 0;
        }
        if (selectedTuto >= tutorialScreens.Length)
        {
            selectedTuto = tutorialScreens.Length - 1;
        }

        InfoTitle.text = $"Tutorial {selectedTuto + 1}/{tutorialScreens.Length}";
        InfoContent.text = tutorialScreens[selectedTuto];

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            // End here
            state = GameStateEnum.inGame;
        }
    }
    public string victoryText;
    public string defeatText;
    void HandleEndScreen()
    {
        InfoPanel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            // End here
            state = GameStateEnum.inGame;
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case GameStateEnum.inTuto:
                HandleTuto();
                return;
            case GameStateEnum.inGameDefeat:
            case GameStateEnum.inGameVictory:
                HandleEndScreen();
                return;
            default:
                break;
        }
        InfoPanel.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Pause))
        {
            setPause(!GameControl.isGamePaused);
        }

        // User input are avoided when game is paused
        if (GameControl.isGamePaused)
        {
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
        cameraDistance = (float)cameraSize / cameraSizeMin;
        float camx = (float)(transform.position.x + dirx);
        float camy = (float)(transform.position.y + diry);

        if (camx < cameraSize)
        {
            dirx = cameraSize - transform.position.x;
        }
        if (camx > map.width - cameraSize)
        {
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

        transform.Translate(new Vector3((float)dirx, (float)diry, 0));
    }
}
