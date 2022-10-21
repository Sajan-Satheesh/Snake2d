using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public GameObject[] PowerUps;
    GameObject power;
    public bool spawn;
    private void Start()
    {
        spawn = true;
        respwan();
    }
    public int RandomPower()
    {
        int Selection = UnityEngine.Random.Range(0, PowerUps.Length);
        return Selection;
    }
    public void respwan()
    {
        if (spawn)
        {
            power = Instantiate(PowerUps[RandomPower()]);
            spawn = false;
        }
        
    }
    public void destroyObj()
    {
        Destroy(power);
    }
}
