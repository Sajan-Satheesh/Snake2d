using UnityEngine;

public class SpawnBounds : MonoBehaviour
{
    public static Bounds bounds;
    BoxCollider2D boxCollider2D;
    // Start is called before the first frame update

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        bounds = boxCollider2D.bounds;
    }
}
