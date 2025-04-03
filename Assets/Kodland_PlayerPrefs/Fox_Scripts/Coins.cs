using UnityEngine;

public class Coins : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Fox_Logic>().coins++;
            gameObject.SetActive(false);
        }
    }
}