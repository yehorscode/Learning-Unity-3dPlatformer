using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Gravity
    [Header("Gravity & Movement")]
    [SerializeField] float gravity = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float moveSpeed = 14f;
    [SerializeField] float sprintSpeed = 20f;
    [SerializeField] float doubleJumpHeight = 3f;
    bool isJumping;
    private Rigidbody rb;
    bool isBlocked = false;
    // If true = then its forward, if false = then its backwards
    bool prevDirection = true;

    // Jump & Sprint
    private bool isGrounded = true;
    private bool isSprinting = false;
    private bool hasDoubleJump = true;
    private float moveVelocity = 0f;
    private float jumpVelocity = 0f;
    private float doubleJumpVelocity = 0f;

    // Dashes
    [Header("Dashes")]
    [SerializeField] float dashLength = 10f;
    [SerializeField] float health = 100f;

    [Header("Cameras")]
    [SerializeField] bool logicOverride = false;
    [SerializeField] Camera forwardCamera;
    [SerializeField] Camera backCamera;
    [Header("Scripts")]
    public ActionsManager actionsManager;
    public TimedLetter timedLetter;
    public PlayerManager playerManager;
    public VignetteEffect vignetteEffect;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        rb = GetComponent<Rigidbody>();
        actionsManager = GetComponent<ActionsManager>();
        timedLetter = GetComponent<TimedLetter>();
        playerManager = GetComponent<PlayerManager>();
        vignetteEffect = GetComponent<VignetteEffect>();
        forwardCamera.enabled = true;
        backCamera.enabled = true;
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Move();
        Jump();
    }

    void Move()
    {
        if (actionsManager.isTimedEvent || playerManager.isDead)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            EnableOnlyCamera(forwardCamera);
            return;
        }

        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * verticalInput;

        // Vignette effect
        if (Input.GetKeyDown(KeyCode.W) && !prevDirection)
        {
            vignetteEffect.ActivateVignette();
        }
        else if (Input.GetKeyDown(KeyCode.S) && prevDirection)
        {
            vignetteEffect.ActivateVignette();
        }

        if (Input.GetButton("Sprint"))
        {
            isSprinting = true;
            moveVelocity = sprintSpeed;
        }
        else
        {
            isSprinting = false;
            moveVelocity = moveSpeed;
        }

        if (verticalInput >= 0)
        {
            prevDirection = true;
            EnableOnlyCamera(forwardCamera);
        }
        else
        {
            prevDirection = false;
            EnableOnlyCamera(backCamera);
        }

        // Dash logic (left & right)
        if (Input.GetKeyDown(KeyCode.A) && !isBlocked) // Dash left
        {
            if (verticalInput >= 0)
            {
                transform.position = new Vector3(transform.position.x - dashLength, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + dashLength, transform.position.y, transform.position.z);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isBlocked) // Dash right
        {
            if (verticalInput >= 0)
            {
                transform.position = new Vector3(transform.position.x + dashLength, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x - dashLength, transform.position.y, transform.position.z);
            }
        }

        rb.velocity = new Vector3(0, rb.velocity.y, movement.z * moveVelocity);
    }

    void Jump()
    {
        if (actionsManager.isTimedEvent)
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                isGrounded = false;
                isJumping = true;
                jumpVelocity = Mathf.Sqrt(jumpHeight * 2 * gravity);
                rb.AddForce(Vector3.up * jumpVelocity, ForceMode.VelocityChange);
            }
            else if (hasDoubleJump)
            {
                hasDoubleJump = false;
                isJumping = true;
                doubleJumpVelocity = Mathf.Sqrt(doubleJumpHeight * 2 * gravity);
                rb.AddForce(Vector3.up * doubleJumpVelocity, ForceMode.VelocityChange);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        hasDoubleJump = true;
        isJumping = false;
        isBlocked = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            isBlocked = true;
        }
        else if (other.tag == "InstaKill")
        {
            playerManager.Die();
        }
    }

    void HideInfoText()
    {
        actionsManager.HideInfoText();
    }

    void EnableOnlyCamera(Camera camToEnable)
    {
        if (camToEnable == null)
        {
            Debug.LogWarning("Camera to enable is null.");
            return;
        }

        DisableAllCameras(camToEnable);
        camToEnable.enabled = true;
    }

    void DisableAllCameras(Camera camToNotDisable)
    {

        if (forwardCamera != null && forwardCamera != camToNotDisable)
        {
            forwardCamera.enabled = false;
        }

        if (backCamera != null && backCamera != camToNotDisable)
        {
            backCamera.enabled = false;
        }
    }

}
