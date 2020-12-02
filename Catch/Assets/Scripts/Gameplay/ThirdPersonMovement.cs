using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Rigidbody playerRB;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    public GameObject fullRotAnchor;
    public GameObject boostEffects;
    public GameObject jumpEffects;
    public GameObject diveEffects;

    public BoostBar boostBar;

    public float moveForce = 400f;
    public float boostForce = 1000f;
    public float jumpImpulse = 70f;
    public float jumpForce = 300f;
    public float diveForce = 300f;
    public float maxBoost = 3f;
    public float rechargeRate = 0.5f;

    float horizontal;
    float vertical;

    bool isGrounded;
    bool isBoosting = false;
    bool isJumping = false;
    bool isDiving = false;
    float boost;

    private void Start()
    {
        boost = maxBoost;
        boostBar.SetMaxBoost(maxBoost);
        
        Cursor.lockState = CursorLockMode.Locked;
    }


    void FixedUpdate()
    {
        Transform cam = Camera.main.transform;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        bool boostUsed = false;

        transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
        fullRotAnchor.transform.rotation = cam.rotation;

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.001f)
        {
            float moveAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            moveDirection = (Quaternion.Euler(0f, moveAngle, 0f) * Vector3.forward).normalized;

            if (isBoosting)
            {
                moveDirection *= boostForce;
                boost -= Time.deltaTime;
                boostUsed = true;

                boostEffects.SetActive(true);
                boostEffects.transform.position = playerRB.position - 0.5f * moveDirection.normalized;
                boostEffects.transform.LookAt(playerRB.transform);
            }
            else
            {
                moveDirection *= moveForce;
            }
        }

        playerRB.AddForce(moveDirection);

        if (isJumping)
        {
            playerRB.AddForce(0f, jumpForce, 0f);
            boost -= Time.deltaTime;
            boostUsed = true;
        }

        if (isDiving)
        {
            playerRB.AddForce(0f, -diveForce, 0f);
            boost -= Time.deltaTime;
            boostUsed = true;
        }

        if (!boostUsed)
        {
            boost += Time.deltaTime * rechargeRate;
            boost = Mathf.Min(boost, maxBoost);
        }
        
        if (boost <=0)
        {
            OnBoostCancel();
            OnJumpCancel();
            OnDiveCancel();
        }

        boostBar.SetBoost(boost);
    }

    public void OnMoveInput(float horizontal, float vertical)
    {
        this.horizontal = horizontal;
        this.vertical = vertical;
    }

    public void OnJumpStart()
    {
        if (isGrounded)
        {
            playerRB.AddForce(new Vector3(0f, jumpImpulse, 0f), ForceMode.Impulse);
        }

        if (boost > 0)
        {
            isJumping = true;
            jumpEffects.SetActive(true);
        }
    }
    
    public void OnJumpCancel()
    {
        isJumping = false;
        jumpEffects.SetActive(false);
    }

    public void OnBoostStart()
    {
        if (boost > 0)
        {
            isBoosting = true;
            boostEffects.SetActive(true);
        }
    }

    public void OnBoostCancel()
    {
        isBoosting = false;
        boostEffects.SetActive(false);
    }

    public void OnDiveStart()
    {
        if (boost > 0)
        {
            isDiving = true;
            diveEffects.SetActive(true);
        }
    }

    public void OnDiveCancel()
    {
        isDiving = false;
        diveEffects.SetActive(false);
    }
}
