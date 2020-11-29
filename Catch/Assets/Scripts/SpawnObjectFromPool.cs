using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class SpawnObjectFromPool : MonoBehaviour
{
    public string poolName;
    public GameObject objectToSpawn;

    public float timeInterval;

    public Vector3 spawnPosJitter;


    float timer = 1f;


    void Start()
    {
        timer = 0;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > timeInterval)
        {
            timer = 0;

            Vector3 spawnPosition = transform.position;
            spawnPosition += Vector3.right * spawnPosJitter.x * (Random.value - 0.5f);
            spawnPosition += Vector3.forward * spawnPosJitter.z * (Random.value - 0.5f);
            spawnPosition += Vector3.up * spawnPosJitter.y * (Random.value - 0.5f);

            EasyObjectPool.instance.GetObjectFromPool(poolName, spawnPosition, Quaternion.identity);
        }
    }
}
