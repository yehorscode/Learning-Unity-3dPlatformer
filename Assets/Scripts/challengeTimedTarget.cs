using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class challengeTimedTarget : MonoBehaviour
{
    ActionsManager actionsManager;
    TimedTarget timedTarget;
    [SerializeField] GameObject player;

    void Start()
    {
        actionsManager = player.GetComponent<ActionsManager>();
        timedTarget = player.GetComponent<TimedTarget>();

        if (player == null)
        {
            Debug.LogError("challengeTimedTarget: Missing player GameObject! Please assign it in the inspector.");
        }
        if (deletingObjects == null || deletingObjects.Count == 0)
        {
            Debug.LogError("challengeTimedTarget: Missing deletingObjects! Please assign them in the inspector.");
        }
        if (aveMaria == null)
        {
            Debug.LogError("challengeTimedTarget: Missing aveMaria AudioClip! Please assign it in the inspector.");
        }
        if (completeSound == null)
        {
            Debug.LogError("challengeTimedTarget: Missing completeSound AudioClip! Please assign it in the inspector.");
        }
    }
    [SerializeField] int neededCorrectClicks = 5;
    [SerializeField] int maxWrongClicks = 3;
    [SerializeField] float gameDuration = 10f;
    [SerializeField] List<GameObject> deletingObjects = new List<GameObject>();
    [SerializeField] AudioClip aveMaria;
    [SerializeField] AudioClip completeSound;
    AudioSource audioSource;

    void Update()
    {
        if (actionsManager.isTimingTargets)
        {
            if (timedTarget.correctClicks >= neededCorrectClicks && timedTarget.wrongClicks <= maxWrongClicks)
            {
                CompleteChallenge();
            }
            else if (timedTarget.wrongClicks > maxWrongClicks)
            {
                FailChallenge();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            actionsManager = other.GetComponent<ActionsManager>();
            timedTarget = other.GetComponent<TimedTarget>();
            
            timedTarget.maxWrongClicks = maxWrongClicks;
            timedTarget.neededCorrectClicks = neededCorrectClicks;
            timedTarget.gameDuration = gameDuration;
            
            timedTarget.StartTargetClicks();
            actionsManager.isTimingTargets = true;
        }
    }
    
    void CompleteChallenge()
    {
        Debug.Log("Challenge completed!");
        actionsManager.isTimingTargets = false;
        timedTarget.EndGame();
        foreach (var obj in deletingObjects)
        {
            var rb = obj.GetComponent<Rigidbody>();
            audioSource = obj.GetComponent<AudioSource>();
            audioSource.clip = completeSound;
            audioSource.volume = 0.8f;
            audioSource.PlayOneShot(completeSound, 1.5f);
            audioSource.volume = 1.4f;
            audioSource.PlayOneShot(aveMaria, 0.8f);
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.up * Random.Range(6f, 8f); // Make objects float up
            }
        }
    }

    void FailChallenge()
    {
        Debug.Log("Challenge failed: too many wrong clicks. Try again!");
        actionsManager.isTimingTargets = false;
        timedTarget.EndGame();
    }
}

