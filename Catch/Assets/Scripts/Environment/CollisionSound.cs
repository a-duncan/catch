using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public AudioSource collisionSound;

    public float relativeVelocityThreshold = 1f;
    public float fullVolumeVelocity = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        float velocity = collision.relativeVelocity.magnitude;

        if (velocity > relativeVelocityThreshold)
        {
            float vol = Mathf.Min(1f, velocity / fullVolumeVelocity);
            collisionSound.PlayOneShot(collisionSound.clip, vol);
        }
    }
}
