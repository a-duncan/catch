using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHailstorm : MonoBehaviour
{
    public GameObject hailSpawner1;
    public GameObject hailSpawner2;


    void Start()
    {
        for (float x = -45f; x < 50f; x += 10f)
        {
            for (float z = -45f; z < 50f; z += 10f)
            {
                Vector3 spawnLoc = new Vector3(x, 0f, z);
                spawnLoc += this.transform.position;
                Instantiate(hailSpawner1, spawnLoc, Quaternion.identity, this.transform);
                Instantiate(hailSpawner2, spawnLoc, Quaternion.identity, this.transform);
            }
        }

    }
}
