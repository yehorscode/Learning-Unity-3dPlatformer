using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Canvas for die screen
    [Header("Death Handling")]
    [SerializeField] GameObject deathCanvas;
    [SerializeField] public bool isDead = false;
    void Start()
    {
        deathCanvas.SetActive(false);
    }
    public void Die()
    {
        deathCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isDead = false;
        Cursor.lockState = CursorLockMode.None;
    }
}

