using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float sprintJumpForce = 6f;
    [SerializeField] float doubleJumpForce = 3f;
    [SerializeField] float sprintSpeed = 20f;
    [SerializeField] float stamina = 5f;
    [SerializeField] float maxStamina = 5f;
    [SerializeField] float staminaRecoveryRate = 1f;
    [SerializeField] float staminaConsumptionRate = 1f;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip doubleJumpSound;
    [SerializeField] AudioClip sprintSound;
    
    bool doubleJumpAvailable;
    Vector3 moveDirection;
    float currentSpeed;
    public bool isGrounded;
    bool isSprinting;
    
    public float Stamina { get { return stamina; } set { stamina = Mathf.Clamp(value, 0, maxStamina); } }
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents unwanted rotation due to collisions
        doubleJumpAvailable = false;
        Physics.gravity = new Vector3(0, -9.81f, 0);
        Stamina = maxStamina;
        
        // Make sure we have AudioSource
        if (source == null)
            source = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        // Check if grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        
        // Reset double jump when grounded
        if (isGrounded)
            doubleJumpAvailable = true;
        
        // Handle input
        // HandleMovement();
        // HandleJump();
        // HandleSprint();
    }
    
    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = (transform.forward * vertical + transform.right * horizontal).normalized;
    }
    
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (doubleJumpAvailable)
            {
                DoubleJump();
            }
        }
    }
    
    void HandleSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Stamina > 0 && moveDirection.magnitude > 0.1f)
        {
            isSprinting = true;
            currentSpeed = sprintSpeed;
            Stamina -= staminaConsumptionRate * Time.deltaTime;
            
            // Play sprint sound if we have one
            if (source != null && sprintSound != null && !source.isPlaying)
                source.PlayOneShot(sprintSound);
        }
        else
        {
            isSprinting = false;
            currentSpeed = playerSpeed;
            
            // Only recover stamina when not sprinting
            if (Stamina < maxStamina)
            {
                Stamina += staminaRecoveryRate * Time.deltaTime;
            }
        }
    }
    
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset Y velocity before jump
        
        // Apply stronger jump if sprinting
        float actualJumpForce = isSprinting ? sprintJumpForce : jumpForce;
        rb.AddForce(Vector3.up * actualJumpForce, ForceMode.Impulse);
        
        // Play jump sound
        if (source != null && jumpSound != null)
            source.PlayOneShot(jumpSound);
    }
    
    void DoubleJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset Y velocity before double jump
        rb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
        doubleJumpAvailable = false;
        
        // Play double jump sound
        if (source != null && doubleJumpSound != null)
            source.PlayOneShot(doubleJumpSound);
    }
    
    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);
    }
}