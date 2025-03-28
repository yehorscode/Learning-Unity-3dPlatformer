using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float gravity = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float moveSpeed = 14f;
    [SerializeField] float sprintSpeed = 20f;
    [SerializeField] float doubleJumpHeight = 3f;

    private bool isGrounded = true;
    private bool isSprinting = false;
    private bool hasDoubleJump = true;
    private float moveVelocity = 0f;
    private float jumpVelocity = 0f;
    private float doubleJumpVelocity = 0f;

    private Rigidbody rb;
    [SerializeField] Camera playerCamera;
    private float xRotation = 0f;

    // Dashes
    [SerializeField] float dashLength = 10f;
    [SerializeField] float health = 100f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
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
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * verticalInput;

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
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - dashLength, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + dashLength, transform.position.y, transform.position.z);
    }

        rb.velocity = new Vector3(0, rb.velocity.y, movement.z * moveVelocity);
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                isGrounded = false;
                jumpVelocity = Mathf.Sqrt(jumpHeight * 2 * gravity);
                rb.AddForce(Vector3.up * jumpVelocity, ForceMode.VelocityChange);
            }
            else if (hasDoubleJump)
            {
                hasDoubleJump = false;
                doubleJumpVelocity = Mathf.Sqrt(doubleJumpHeight * 2 * gravity);
                rb.AddForce(Vector3.up * doubleJumpVelocity, ForceMode.VelocityChange);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        hasDoubleJump = true;
    }
}