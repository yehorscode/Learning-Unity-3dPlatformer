using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Transform player; // Reference to the player's position
    [SerializeField] private float edgeDistance = 5.0f; // Distance to detect edges
    [SerializeField] private float jumpForce = 5.0f; // Force applied during jump
    private NavMeshAgent navAgent;
    private Rigidbody rb;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player != null)
        {
            navAgent.SetDestination(player.position);
        }

        if (IsNearEdge())
        {
            Jump();
        }
    }

    private bool IsNearEdge()
    {
        // Raycast downwards to check if near edge
        RaycastHit hit;
        return !Physics.Raycast(transform.position, Vector3.down, out hit, edgeDistance);
    }

    private void Jump()
    {
        // Apply a force to make the enemy jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}

