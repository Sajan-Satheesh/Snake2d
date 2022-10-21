using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject player2;
    public GameObject boundedMap;
    private void Awake()
    {
        if (GameManager.gameMode == GameModes.MultiPlayer)
        {
            player2.SetActive(true);
        }
        else player2.SetActive(false);
        if (GameManager.mapMode == MapModes.Bounded)
        {
            boundedMap.SetActive(true);
        }
        else boundedMap.SetActive(false);
    }
}
