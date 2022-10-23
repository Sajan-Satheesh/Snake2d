using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected Vector2 RandomPosition()
    {
        float randX = Random.Range(SpawnBounds.bounds.min.x, SpawnBounds.bounds.max.x);
        float randY = Random.Range(SpawnBounds.bounds.min.y, SpawnBounds.bounds.max.y);
        Vector2 randomPosition = new Vector2(Mathf.Round(randX), Mathf.Round(randY));
        return randomPosition;
    }
}
