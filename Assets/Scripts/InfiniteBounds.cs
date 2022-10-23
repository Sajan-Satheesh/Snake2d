using UnityEngine;

public class InfiniteBounds : MonoBehaviour
{
    public static Bounds ibounds;
    BoxCollider2D boxCollider2D;
    // Start is called before the first frame update

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        ibounds = boxCollider2D.bounds;
    }
}
