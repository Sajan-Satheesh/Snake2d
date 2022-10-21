
using UnityEngine;

public enum CollectibleType
{
    MassGainer,
    MassBurner,
}

public class Collectible : MonoBehaviour
{

    public CollectibleType collectibleType;
    void Start()
    {
        this.transform.position = RandomPosition();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerController))
        {
            transform.position = RandomPosition();
            switch (collectibleType)
            {
                case CollectibleType.MassGainer:

                    break;
                case CollectibleType.MassBurner:
                    break;
            }
        }
    }

    private Vector2 RandomPosition()
    {
        float randX = UnityEngine.Random.Range(SpawnBounds.bounds.min.x, SpawnBounds.bounds.max.x);
        float randY = UnityEngine.Random.Range(SpawnBounds.bounds.min.y, SpawnBounds.bounds.max.y);
        Vector2 randomPosition = new Vector2(Mathf.Round(randX), Mathf.Round(randY));
        return randomPosition;
    }


}
