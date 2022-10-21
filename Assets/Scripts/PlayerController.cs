using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float defaultSpeed;
    public static float speed;
    Directions direction;
    public ObjType objType;
    public GameObject GameOverScreen;
    Vector2 PlayerPosition;
    GameState gameState;
    [SerializeField] private List<Transform> SegmentPosition;
    [SerializeField] private Transform body;
    [SerializeField] private int defaultBodyLength;
    public KeyCode UpButton;
    public KeyCode DownButton;
    public KeyCode LeftButton;
    public KeyCode RightButton;
    public UiUpdate uiUpdate;

    private enum Directions
    {
        up,
        down,
        left,
        right
    }
    private enum GameState
    {
        paused,
        play
    }


    private void Awake()
    {
        timer = true;
        speed = defaultSpeed;
        gameState = GameState.play;
        SegmentPosition = new List<Transform>();
        SegmentPosition.Add(transform);
        NewSnake(defaultBodyLength);
        
        Time.timeScale = 1;
        Time.fixedDeltaTime = speed;
        direction = Directions.up;
        objType = ObjType.player;

    }

    private void NewSnake(int length)
    {
        for (int i = 0; i < defaultBodyLength; i++)
        {
            growSnake();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        runningTime = (int)Time.time;
        GetDirection();
        CheckBoundary();
        pause();
        CoolDown();
    }
    void FixedUpdate()
    {
        UpdateBody();
        Run();
    }

    private void pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState == GameState.play) 
            {
                Time.timeScale = 0; 
                gameState = GameState.paused;
            } 
            else
            {
                Time.timeScale = 1; 
                gameState = GameState.play;
            } 
        }
    }


    static int PowerActiveTime = 10;
    int currentTime;
    int runningTime;
    GameObject PowerObject;
    bool coolDown;
    bool rusher;
    bool ShieldMode;
    bool timer;
    public bool ate;
    public PowerUpSpawn powerup;
    bool gameModeReversed = false;

    void DestroyGameObjects()
    {
        Destroy(gameObject);
        for (int i = 0; i < SegmentPosition.Count; i++)
        {
            Destroy(SegmentPosition[i].gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Death death))
        {
            if (!ShieldMode &&(death.objType == ObjType.boundary || death.objType == ObjType.player))
            {
                if (GameManager.gameMode == GameModes.MultiPlayer)
                {
                    GameManager.gameMode = GameModes.SinglePlayer;
                    gameModeReversed = true;
                    DestroyGameObjects();
                }
                else
                {
                    if (gameModeReversed)
                    {
                        GameManager.gameMode = GameModes.MultiPlayer;
                    }
                    Time.timeScale = 0;
                    GameOverScreen.SetActive(true); 
                }
                    
            }
        }
        if (collision.gameObject.TryGetComponent(out Collectible collectible))
        {
            if(collectible.collectibleType == CollectibleType.MassGainer)
            {
                growSnake();
                uiUpdate.score += ScoringDatas.gainScore;
                uiUpdate.UpdateScore();
            }
            if (collectible.collectibleType == CollectibleType.MassBurner)
            {
                reduceSnake();
                uiUpdate.score += ScoringDatas.loseScore;
                uiUpdate.UpdateScore();
            }
        }
        if (collision.gameObject.TryGetComponent(out PowerUp powerUp))
        {
            
            PowerActiveTime = UnityEngine.Random.Range(5, 15);
            currentTime = (int)Time.time;
            PowerObject = powerUp.gameObject;
            if (powerUp.powerType == PowerType.SpeedUp)
            {
                /*powerUp.gameObject.SetActive(false);*/
                rusher = true;
                PlayerController.speed = 0.05f;
                Time.fixedDeltaTime = PlayerController.speed;
                CoolDownActivate();
            }
            else if (powerUp.powerType == PowerType.Shield)
            {
                /*powerUp.gameObject.SetActive(false);*/
                ShieldMode = true;
                CoolDownActivate();
            }
            else if (powerUp.powerType == PowerType.ScoreBoost)
            {
                /*powerUp.gameObject.SetActive(false);*/
                uiUpdate.score += ScoringDatas.boostScore;
                uiUpdate.UpdateScore();
                CoolDownActivate();
                timer = false;
            }
            powerup.destroyObj();
        }
    }
    private void CoolDownActivate()
    {
        coolDown = true; 
    }

    private void CoolDown()
    {
        if (coolDown)
        {
            if (timer)
            {
                uiUpdate.CountDown.enabled = true;
                uiUpdate.timer = PowerActiveTime - (runningTime - currentTime);
                uiUpdate.CountDownTimer();
            }
            
            if (runningTime - currentTime > PowerActiveTime)
            {
                powerup.spawn = true;
                powerup.respwan();
                speed = defaultSpeed;
                Time.fixedDeltaTime = speed;
                ShieldMode = false;
                coolDown = false;
                rusher = false;
                uiUpdate.CountDown.enabled = false;
                timer = true;
            }
        }
    }

    private void reduceSnake()
    {
        if (SegmentPosition.Count > 3)
        {
            Destroy(SegmentPosition[SegmentPosition.Count - 1].gameObject);
            SegmentPosition.Remove(SegmentPosition[SegmentPosition.Count - 1]);
        }
        
    }

    private void growSnake()
    {
        SegmentPosition.Add(Instantiate(body));
    }

    private void CheckBoundary()
    {
        int objX = (int)transform.position.x;
        int objY = (int)transform.position.y;

        if (objX > SpawnBounds.bounds.max.x)
        {
            objX = (int)SpawnBounds.bounds.min.x;
        }
        else if (objX < SpawnBounds.bounds.min.x)
        {
            objX = (int)SpawnBounds.bounds.max.x;
        }
        if (objY > SpawnBounds.bounds.max.y)
        {
            objY = (int)SpawnBounds.bounds.min.y;
        }
        else if (objY < SpawnBounds.bounds.min.y)
        {
            objY = (int)SpawnBounds.bounds.max.y;
        }
        transform.position = new Vector3Int(objX, objY,0);
    }
    private void GetDirection()
    {
        
        if (Input.GetKey(UpButton) && direction != Directions.down)
        {
            direction = Directions.up;
        }
        if (Input.GetKey(DownButton) && direction != Directions.up)
        {
            direction = Directions.down;
        }
        if (Input.GetKey(LeftButton) && direction != Directions.right)
        {
            direction = Directions.left;
        }
        if (Input.GetKey(RightButton) && direction != Directions.left)
        {
            direction = Directions.right;
        }
        if (!rusher && (Input.GetKeyDown(UpButton) || Input.GetKeyDown(DownButton) || Input.GetKeyDown(LeftButton) || Input.GetKeyDown(RightButton)))
        {
            Time.fixedDeltaTime = 0.1f;
        }
        else if (Input.GetKeyUp(UpButton) || Input.GetKeyUp(DownButton) || Input.GetKeyUp(LeftButton) || Input.GetKeyUp(RightButton))
        {
            Time.fixedDeltaTime = speed;
        }

    }
    private void Run()
    {
        PlayerPosition = transform.position;
        if (direction == Directions.up)
        {
            transform.position = PlayerPosition + Vector2.up;
        }
        if (direction == Directions.down)
        {
            transform.position = PlayerPosition + Vector2.down;
        }
        if (direction == Directions.left)
        {
            transform.position = PlayerPosition + Vector2.left;
        }
        if (direction == Directions.right)
        {
            transform.position = PlayerPosition + Vector2.right;
        }
    }

    private void UpdateBody()
    {
        for (int i = SegmentPosition.Count - 1; i > 0 ; i--)
        {
            SegmentPosition[i].position = SegmentPosition[i - 1].position;
        }
    }
}
