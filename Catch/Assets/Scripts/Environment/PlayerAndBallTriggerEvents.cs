using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerAndBallTriggerEvents : MonoBehaviour
{
    public UnityEvent playerEvent;
    public UnityEvent ballEvent;


    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                playerEvent.Invoke();
                break;
            case "Ball":
                ballEvent.Invoke();
                break;
            default:
                break;
        }
    }
}
