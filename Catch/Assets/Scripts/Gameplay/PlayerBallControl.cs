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
    public float grabRadius = 5f;
    public float grabCapsuleInnerLen = 10;
    public float holdRadius = 5f;

    public float grabStrength = 400f;
    public float grabDamping = 20f;

    public float throwBaseImpulse = 300f;

    public float pullStrength = 400f;
    public float releaseStrength = 100f;

    bool isGrabbing = false;
    bool isPulling = false;
    List<Rigidbody> grabbedRBs;
    List<Vector3> grabbedOffsets;


    private void FixedUpdate()
    {

        if (isGrabbing)
        {
            Quaternion camRotate = Camera.main.transform.rotation;

            List<int> ungrabbedIndices = new List<int>();
            for (int i = 0; i < grabbedRBs.Count; i++)
            {
                Vector3 grabPosition = playerRB.position + camRotate * grabbedOffsets[i];
                float holdDistance = (grabPosition - grabbedRBs[i].position).magnitude;

                if (holdDistance > holdRadius && !isPulling)
                {
                    ungrabbedIndices.Add(i);
                }
                else
                {
                    grabbedRBs[i].AddForce(-grabDamping * grabbedRBs[i].velocity);

                    float holdForce = grabStrength * holdDistance;
                    grabbedRBs[i].AddExplosionForce(-holdForce, grabPosition, 0f);
                }

                if (isPulling)
                {
                    float pullForce = (grabbedRBs[i].position - throwCenter.position).magnitude;
                    pullForce *= pullStrength;
                    grabbedRBs[i].AddExplosionForce(-pullForce, throwCenter.position, 0f);
                }
            }

            ungrabbedIndices.Reverse();
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

        Quaternion camRotate = Camera.main.transform.rotation;
        Vector3 forwardCapsulePoint = grabCapsuleInnerLen * Vector3.forward;
        forwardCapsulePoint = playerRB.position + camRotate * forwardCapsulePoint;

        Collider[] grabColliders = Physics.OverlapCapsule(playerRB.position, forwardCapsulePoint, grabRadius, grabLayer);

        if (grabColliders.Length > 0)
        {
            isGrabbing = true;
            Debug.Log("Grabbing:" + grabColliders.Length.ToString());

            grabbedRBs = new List<Rigidbody>();
            grabbedOffsets = new List<Vector3>();

            Quaternion camUnrotate = Quaternion.Inverse(camRotate);

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
                float grabDistance = (grabbedRB.position - playerRB.position).magnitude;
                float maxDistance = grabCapsuleInnerLen + grabRadius;

                if (grabDistance < maxDistance)
                {
                    Vector3 throwForce = (grabbedRB.position - throwCenter.position).normalized;
                    throwForce *= throwBaseImpulse * (1 - grabDistance / maxDistance);
                    grabbedRB.AddForce(throwForce, ForceMode.Impulse);
                }
            }
        }

    }

    public void StartPulling()
    {
        isPulling = true;
    }

    public void ReleasePulling()
    {
        if (isGrabbing)
        {
            isGrabbing = false;
            isPulling = false;

            Quaternion camRotate = Camera.main.transform.rotation;
            for (int i = 0; i < grabbedRBs.Count; i++)
            {
                Vector3 grabPosition = playerRB.position + camRotate * grabbedOffsets[i];
                Vector3 throwForce = releaseStrength * (grabPosition - grabbedRBs[i].position);

                grabbedRBs[i].AddForce(throwForce, ForceMode.Impulse);
            }
        }
    }
}
