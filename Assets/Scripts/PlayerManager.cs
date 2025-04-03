using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] AudioSource audioSource;

    // Canvas for die screen
    [Header("Death Handling")]
    [SerializeField] GameObject deathCanvas;
    [SerializeField] public bool isDead = false;
    [SerializeField] AudioClip deathSound;
    void Start()
    {
        deathCanvas.SetActive(false);
    }
    public void Die()
    {
        deathCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        audioSource.clip = deathSound;
        audioSource.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isDead = false;
        Cursor.lockState = CursorLockMode.None;
    }
}

