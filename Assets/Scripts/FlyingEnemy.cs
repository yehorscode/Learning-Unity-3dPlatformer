using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] public Transform player; // Reference to the player's position
    [SerializeField] private float speed = 5.0f; // Speed of the enemy
    [SerializeField] private float hoverDistance = 2.0f; // Distance to hover around the player
    public bool isFlying = false; // Toggle for flying behavior

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable gravity for flying behavior
    }

    void Update()
    {
        if (isFlying)
        {
            FlyTowardsPlayer();
        }
    }

    private void FlyTowardsPlayer()
    {
        // Calculate direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Maintain a hover distance from the player
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > hoverDistance)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector3.zero; // Stop moving when within hover distance
        }

        // Rotate to face the player
        transform.LookAt(player);
    }

    public void ToggleFlying()
    {
        isFlying = !isFlying;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}

