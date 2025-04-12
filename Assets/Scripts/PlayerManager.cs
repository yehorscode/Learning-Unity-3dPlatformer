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

    [Header("Respawn Handling")]
    [SerializeField] public Vector3 respawnPoint;

    void Start()
    {   
        respawnPoint = player.transform.position;
        deathCanvas.SetActive(false);
    }

    public void Die()
    {
        deathCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        audioSource.clip = deathSound;
        audioSource.Play();
    }

    public void RespawnAtSpawnPoint(Vector3 spawnPoint)
    {
        player.transform.position = spawnPoint;
        isDead = false;
        Cursor.lockState = CursorLockMode.None;
        deathCanvas.SetActive(false);
    }

    public void Restart()
    {
        Cursor.lockState = CursorLockMode.None;
        isDead = false;
        player.transform.position = respawnPoint;
        deathCanvas.SetActive(false);
    }
}

