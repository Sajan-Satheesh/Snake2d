
public enum PowerType
{
    Shield,
    ScoreBoost,
    SpeedUp,
    none
}

public class PowerUp : Collectible
{
    public PowerType powerType;
    
    void Start()
    {
        transform.position = RandomPosition();
    }
}
