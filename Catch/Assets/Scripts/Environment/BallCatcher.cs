using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCatcher : MonoBehaviour
{
    public float grabStrength = 500f;
    public float grabDamping = 20f;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Rigidbody ballRB = other.attachedRigidbody;

            ballRB.AddForce(-grabDamping * ballRB.velocity);

            float holdForce = grabStrength * (ballRB.position - transform.position).magnitude;
            ballRB.AddExplosionForce(-holdForce, transform.position, 0f);
        }
    }
}
