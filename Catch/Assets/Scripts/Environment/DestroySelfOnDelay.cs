using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfOnDelay : MonoBehaviour
{
    public float delayTime = 1f;

    float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = delayTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            Destroy(this);
        }
    }
}
