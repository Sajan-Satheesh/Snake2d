
public enum GameModes
{
    SinglePlayer,
    MultiPlayer
}
public enum MapModes
{
    Boundless,
    Bounded
}

public static class GameManager 
{
    public static GameModes gameMode = GameModes.SinglePlayer;
    public static MapModes mapMode = MapModes.Boundless;
}
