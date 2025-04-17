using UnityEngine;

public class CenterPlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        print("CENTERING PLAYER");
        other.transform.position = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
    }
}