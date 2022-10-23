using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;
    private float speed;
    private float TimeUpdate;
    private float CollisionStartTime;
    private Vector2 PlayerPosition;

    public ObjType objType;
    private Directions direction;
    public KeyCode UpButton;
    public KeyCode DownButton;
    public KeyCode LeftButton;
    public KeyCode RightButton;

    [SerializeField] private List<Transform> SegmentPosition;
    [SerializeField] private Transform bodyPosition;
    [SerializeField] private int defaultBodyLength;
    
    public UImanager gameUI;
    

    private enum Directions
    {
        up,
        down,
        left,
        right
    }
   
    private void Awake()
    {
        objType = ObjType.player;
        shieldMode = false;
        timer = true;

        Time.timeScale = 1;
        TimeUpdate = Time.time;
        CollisionStartTime = Time.time + 1;

        InitializeSnake(defaultBodyLength);
        direction = Directions.up;
        speed = defaultSpeed;
    }


    // Update is called once per frame
    private void Update()
    {
        GetDirection();
        TimelyUpdate(speed);
        CoolDown();
    }

    private void InitializeSnake(int length)
    {
        SegmentPosition = new List<Transform>
        {
            transform
        };
        for (int i = 0; i < length; i++)
        {
            if (i > 0)
            {
                GrowSnake();
            }
        }
    }
    private void TimelyUpdate(float speed = 5f)
    {
        float ReqTime = 1 / speed;
        if (TimeUpdate < Time.time)
        {
            
            Run();
            CheckBoundary();
            UpdateBody();
            
            TimeUpdate = Time.time + ReqTime;
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
        if (Time.time>CollisionStartTime)
        {
            collidable = true;
        }
        for (int i = SegmentPosition.Count - 1; i > 0; i--)
        {
            SegmentPosition[i].position = SegmentPosition[i - 1].position;
        }
    }
    
    private void GrowSnake()
    {
        SegmentPosition.Add(Instantiate(bodyPosition));
    }

    private void ReduceSnake()
    {
        if (SegmentPosition.Count > 3)
        {
            Destroy(SegmentPosition[SegmentPosition.Count - 1].gameObject);
            SegmentPosition.Remove(SegmentPosition[SegmentPosition.Count - 1]);
        }

    }

    void DestroyGameObjects()
    {
        Destroy(gameObject);
        for (int i = 0; i < SegmentPosition.Count; i++)
        {
            Destroy(SegmentPosition[i].gameObject);
        }
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
            speed = 10.0f;
        }
        else if (!rusher && (Input.GetKeyUp(UpButton) || Input.GetKeyUp(DownButton) || Input.GetKeyUp(LeftButton) || Input.GetKeyUp(RightButton)))
        {
            speed = defaultSpeed;
        }
    }

    private void CheckBoundary()
    {
        if (!collidable || shieldMode || GameManager.mapMode == MapModes.Boundless)
        {
            int objX = (int)transform.position.x;
            int objY = (int)transform.position.y;

            if (objX > InfiniteBounds.ibounds.max.x)
            {
                objX = (int)InfiniteBounds.ibounds.min.x;
            }
            else if (objX < InfiniteBounds.ibounds.min.x)
            {
                objX = (int)InfiniteBounds.ibounds.max.x;
            }
            if (objY > InfiniteBounds.ibounds.max.y)
            {
                objY = (int)InfiniteBounds.ibounds.min.y;
            }
            else if (objY < InfiniteBounds.ibounds.min.y)
            {
                objY = (int)InfiniteBounds.ibounds.max.y;
            }

            transform.position = new Vector3Int(objX, objY, 0);
        }
    }







    public PowerUpSpawn powerup;
    int PowerActiveTime = 10;
    int currentTime;
    bool coolDown;
    bool rusher;
    bool shieldMode;
    bool timer;
    bool gameModeReversed = false;
    bool collidable;

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collidable)
        {
            if (collision.gameObject.TryGetComponent(out Death death))
            {
                if (!shieldMode && (death.objType == ObjType.boundary || death.objType == ObjType.player))
                {
                    Debug.Log("dead");
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
                        SceneController.gameState = GameState.gameover;
                    }

                }
            }
        }
       
        if (collision.gameObject.TryGetComponent(out Food food))
        {
            if(food.foodType == FoodType.MassGainer)
            {
                GrowSnake();
                gameUI.score += ScoringDatas.gainScore;
                gameUI.UpdateScore();
            }
            if (food.foodType == FoodType.MassBurner)
            {
                ReduceSnake();
                gameUI.score += ScoringDatas.loseScore;
                gameUI.UpdateScore();
            }
        }

        if (collision.gameObject.TryGetComponent(out PowerUp powerUp))
        {
            PowerActiveTime = UnityEngine.Random.Range(5, 15);
            currentTime = (int)Time.time;
            if (powerUp.powerType == PowerType.SpeedUp)
            {
                rusher = true;
                speed = 25.0f;
            }
            else if (powerUp.powerType == PowerType.Shield)
            {
                shieldMode = true;
            }
            else if (powerUp.powerType == PowerType.ScoreBoost)
            {
                gameUI.score += ScoringDatas.boostScore;
                gameUI.UpdateScore();
                timer = false;
            }
            CoolDownActivate();
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
                gameUI.CountDown.enabled = true;
                gameUI.timer = PowerActiveTime - ((int)Time.time - currentTime);
                gameUI.CountDownTimer();
            }
            
            if ((int)Time.time - currentTime > PowerActiveTime)
            {
                powerup.spawn = true;
                powerup.respwan();
                speed = defaultSpeed;
                shieldMode = false;
                coolDown = false;
                rusher = false;
                gameUI.CountDown.enabled = false;
                timer = true;
            }
        }
    }

}
