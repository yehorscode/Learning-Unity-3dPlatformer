using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBorder : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerManager.Die();
        }
        else
        {
            other.gameObject.SetActive(false);
        }
    }
}

