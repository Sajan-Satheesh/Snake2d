
using UnityEngine;

public enum FoodType
{
    MassGainer,
    MassBurner,
}

public class Food : Collectible
{

    public FoodType foodType;
    void Start()
    {
        transform.position = RandomPosition();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerController))
        {
            transform.position = RandomPosition();
        }
    }
}
