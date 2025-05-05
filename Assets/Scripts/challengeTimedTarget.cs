using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class challengeTimedTarget : MonoBehaviour
{
    ActionsManager actionsManager;
    TimedTarget timedTarget;
    [SerializeField] GameObject player;
    private List<Rigidbody> floatingObjects = new List<Rigidbody>();
    private bool hasCompletedChallenge = false;
    [SerializeField] int neededCorrectClicks = 5;
    [SerializeField] int maxWrongClicks = 3;
    [SerializeField] float gameDuration = 10f;
    [SerializeField] List<GameObject> deletingObjects = new List<GameObject>();
    [SerializeField] AudioClip aveMaria;
    [SerializeField] AudioClip completeSound;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        actionsManager = player.GetComponent<ActionsManager>();
        timedTarget = player.GetComponent<TimedTarget>();

        hasCompletedChallenge = false;

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

    void Update()
    {
        if (actionsManager != null && timedTarget != null && actionsManager.isTimingTargets)
        {
            if (!hasCompletedChallenge && timedTarget.HasMetClickRequirements())
            {
                CompleteChallenge();
            }
            else if (!hasCompletedChallenge && (timedTarget.HasExceededWrongClicks() || timedTarget.gameTimer <= 0))
            {
                FailChallenge();
            }
        }

        // Apply continuous force to floating objects
        foreach (var rb in floatingObjects)
        {
            if (rb != null)
            {
                rb.AddForce(Vector3.up * 5f + Random.insideUnitSphere * 2f, ForceMode.Force);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasCompletedChallenge)
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
        if (hasCompletedChallenge) return;

        hasCompletedChallenge = true;
        Debug.Log("Challenge completed!");
        actionsManager.isTimingTargets = false;
        timedTarget.EndGame();

        if (audioSource != null)
        {
            audioSource.PlayOneShot(completeSound, 1.5f);
            audioSource.PlayOneShot(aveMaria, 0.8f);
        }

        StartFloatingObjects();
    }

    void StartFloatingObjects()
    {
        floatingObjects.Clear();
        foreach (var obj in deletingObjects)
        {
            if (obj != null)
            {
                var rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.useGravity = false;
                    rb.velocity = Vector3.up * Random.Range(4f, 6f);
                    rb.angularVelocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
                    floatingObjects.Add(rb);
                }
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

