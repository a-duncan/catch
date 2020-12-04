using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInPlayArea : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Rigidbody otherRB = other.attachedRigidbody;

        switch (other.gameObject.tag)
        {
            case "Player":
                Debug.Log("Player Reset");
                otherRB.velocity = Vector3.zero;
                otherRB.position = 0.5f * Vector3.up;
                break;
            case "Ball":
                Debug.Log("Ball Reset");
                otherRB.velocity = Vector3.zero;
                otherRB.position = new Vector3(5f, 1.5f, 5f);
                break;
            default:
                break;
        }
    }
}
