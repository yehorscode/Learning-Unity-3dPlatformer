using UnityEngine;

public class Jumppad : MonoBehaviour
{
    [SerializeField] float jumpForce = 20f;
    [SerializeField] Vector3 jumpDirection = Vector3.up;
    [SerializeField] bool resetVelocity = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered jumppad");
            
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            
            if (playerRigidbody != null)
            {
                // Optionally reset velocity to prevent additive effects
                if (resetVelocity)
                {
                    playerRigidbody.velocity = Vector3.zero;
                }
                
                // Apply the jump force in the specified direction
                playerRigidbody.AddForce(jumpDirection.normalized * jumpForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("Player does not have a Rigidbody component");
            }
        }
    }
    
    // Optional: Visualize the jump direction in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, jumpDirection.normalized * 2);
    }
}