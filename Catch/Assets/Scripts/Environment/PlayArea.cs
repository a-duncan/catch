using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    public UnityEvent playerLeaves;
    public UnityEvent ballLeaves;

    private void OnTriggerExit(Collider other)
    {
        Rigidbody otherRB = other.attachedRigidbody;

        switch (other.gameObject.tag)
        {
            case "Player":
                playerLeaves.Invoke();
                break;
            case "Ball":
                ballLeaves.Invoke();
                break;
            default:
                break;
        }
    }
}
