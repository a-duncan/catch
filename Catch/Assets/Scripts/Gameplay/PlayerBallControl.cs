using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallControl : MonoBehaviour
{
    public Rigidbody playerRB;

    public Transform holdCenter;
    public Transform throwCenter;

    public float hitMultiplier = 0.2f;

    public LayerMask grabLayer;
    public GameObject grabberGraphics;
    public float grabRadius = 10;
    public float holdRadius = 10;
    public float grabStrength = 2000f;
    public float grabDamping = 20f;

    public float throwBaseImpulse = 300f;

    bool isGrabbing = false;
    List<Rigidbody> grabbedRBs;
    List<Vector3> grabbedOffsets;


    private void FixedUpdate()
    {

        if (isGrabbing)
        {
            Quaternion camRotate = Camera.main.transform.rotation;

            int ct = grabbedRBs.Count;

            List<int> ungrabbedIndices = new List<int>();
            for (int i = 0; i < ct; i++)
            {
                Vector3 grabPosition = playerRB.position + camRotate * grabbedOffsets[i];
                float holdDistance = (grabPosition - grabbedRBs[i].position).magnitude;
                
                if (holdDistance > holdRadius)
                {
                    ungrabbedIndices.Add(i);
                }
                else
                {
                    grabbedRBs[i].AddForce(-grabDamping * grabbedRBs[i].velocity);

                    float holdForce = grabStrength * holdDistance / holdRadius;
                    grabbedRBs[i].AddExplosionForce(-holdForce, grabPosition, 0f);
                }
            }

            foreach (int i in ungrabbedIndices)
            {
                grabbedRBs.RemoveAt(i);
                grabbedOffsets.RemoveAt(i);
            }

            if (grabbedRBs.Count <= 0)
            {
                isGrabbing = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ball")
        {
            Debug.Log("Player hit the ball: " + playerRB.velocity.magnitude.ToString());

            Rigidbody ballRB = collision.collider.attachedRigidbody;
            float hitImpulse = hitMultiplier * (playerRB.velocity - ballRB.velocity).magnitude;

            ballRB.AddExplosionForce(hitImpulse, transform.position, 0f, 0f, ForceMode.Impulse);
        }
        
    }


    public void StartGrabbing()
    {
        grabberGraphics.SetActive(true);

        Collider[] grabColliders = Physics.OverlapSphere(transform.position, grabRadius, grabLayer);

        if (grabColliders.Length > 0)
        {
            isGrabbing = true;
            Debug.Log("Grabbing:" + grabColliders.Length.ToString());

            grabbedRBs = new List<Rigidbody>();
            grabbedOffsets = new List<Vector3>();

            Quaternion camUnrotate = Quaternion.Inverse(Camera.main.transform.rotation);

            foreach (Collider grabCollider in grabColliders)
            {
                Rigidbody grabbedRB = grabCollider.attachedRigidbody;
                grabbedRBs.Add(grabbedRB);

                Vector3 grabbedOffset = (grabbedRB.position - playerRB.position);
                grabbedOffset = camUnrotate * grabbedOffset;
                grabbedOffsets.Add(grabbedOffset);
            }
        }
    }

    public void StopGrabbing()
    {
        grabberGraphics.SetActive(false);
        isGrabbing = false;
    }

    public void ThrowGrabbedObjects()
    {
        if (isGrabbing)
        {
            isGrabbing = false;
            foreach (Rigidbody grabbedRB in grabbedRBs)
            {
                float holdDistance = (grabbedRB.position - playerRB.position).magnitude;
                if (holdDistance < holdRadius)
                {
                    Vector3 throwForce = (grabbedRB.position - throwCenter.position).normalized;
                    throwForce *= throwBaseImpulse * (1 - holdDistance / holdRadius);
                    grabbedRB.AddForce(throwForce, ForceMode.Impulse);
                }
            }
        }

    }


}
