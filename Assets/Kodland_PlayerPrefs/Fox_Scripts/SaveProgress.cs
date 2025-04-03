using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveProgress : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 pos = other.transform.position;
            PlayerPrefs.SetFloat("X", pos[0]);
            PlayerPrefs.SetFloat("Y", pos[1]);
            PlayerPrefs.SetFloat("Z", pos[2]);
            PlayerPrefs.SetInt("Coins", other.gameObject.GetComponent<Fox_Logic>().coins);
            PlayerPrefs.Save();
        }
    }
}
