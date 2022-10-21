
using UnityEngine;

public enum PowerType
{
    Shield,
    ScoreBoost,
    SpeedUp,
    none
}

public class PowerUp : MonoBehaviour
{

    public PowerType powerType;
    public PowerType activePower;
    public BoxCollider2D BoxCollider2D;
    
    void Start()
    {
        
        activePower = PowerType.none;
        transform.position = RandomPosition();
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void ActivatePower(PowerType powerType)
    {
        activePower = powerType;
    }

    private Vector2 RandomPosition()
    {
        float randX = Random.Range(SpawnBounds.bounds.min.x, SpawnBounds.bounds.max.x);
        float randY = Random.Range(SpawnBounds.bounds.min.y, SpawnBounds.bounds.max.y);
        Vector2 randomPosition = new Vector2(Mathf.Round(randX), Mathf.Round(randY));
        return randomPosition;
    }
}
