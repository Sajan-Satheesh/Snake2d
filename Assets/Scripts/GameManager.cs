using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
