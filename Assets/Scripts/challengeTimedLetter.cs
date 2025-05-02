using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class challengeTimedLetter : MonoBehaviour
{
    [SerializeField] float neededCorrectClicks;
    [SerializeField] float maxWrongClicks;
    [SerializeField] AudioClip completeSound;
    [SerializeField] AudioClip floatingSound;
    [Header("Option1: Deleting objects")]
    [SerializeField] List<GameObject> objectsToDelete;

    [Header("Option2: Moving objects out of the way")]
    [SerializeField] List<GameObject> objectsToMove;

    [Header("Particles")]
    [SerializeField] ParticleSystem doneParticle;

    [Header("Player")]
    [SerializeField] ActionsManager actionsManager;
    [SerializeField] TimedLetter letter;
    [SerializeField] GameObject player;

    bool triggered = false;
    bool isActive = false;
    bool challengeCompleted = false;
    bool canRepeat = true;

    void Start()
    {
        if (objectsToMove != null && objectsToMove.Count > 0)
        {
            foreach (var obj in objectsToMove)
            {
                var rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (triggered || challengeCompleted || !canRepeat) return;

        if (other.gameObject == player)
        {
            triggered = true;
            isActive = true;

            if (actionsManager == null || letter == null)
            {
                Debug.LogError("ChallengeTimedLetter: Missing components!");
                triggered = false;
                isActive = false;
                return;
            }

            actionsManager.isTimingLetters = true;
        }
    }

    void Update()
    {
        if (!isActive || challengeCompleted || !canRepeat) return;

        (int correctClicks, int wrongClicks) = letter.GetClicks();

        if (correctClicks >= neededCorrectClicks && wrongClicks <= maxWrongClicks)
        {
            CompleteChallenge();
        }
        else if (wrongClicks > maxWrongClicks)
        {
            FailChallenge();
        }
    }

    void CompleteChallenge()
    {
        challengeCompleted = true;
        canRepeat = false;
        isActive = false;
        actionsManager.isTimingLetters = false;

        if (objectsToDelete != null && objectsToDelete.Count > 0)
        {
            foreach (var obj in objectsToDelete)
            {
                Destroy(obj);
            }
        }
        else if (objectsToMove != null && objectsToMove.Count > 0)
        {
            foreach (var obj in objectsToMove)
            {
                var rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    var direction = new Vector3(Random.Range(-1f, 1f), 0, 0);
                    rb.velocity = direction * Random.Range(0.5f, 1.5f);
                    StartCoroutine(StopMovingAfterDelay(obj, 15f));
                }
                else
                {
                    Debug.LogError("ChallengeTimedLetter: Missing Rigidbody on object: " + obj.name);
                }

                AudioSource objAudio = obj.GetComponent<AudioSource>();
                if (objAudio != null)
                {
                    objAudio.clip = floatingSound;
                    objAudio.loop = true;
                    objAudio.Play();
                }
                else
                {
                    Debug.LogError("ChallengeTimedLetter: Missing AudioSource on object: " + obj.name);
                }
            }
        }
        else
        {
            Debug.LogWarning("ChallengeTimedLetter: No objects to delete or move.");
        }

        if (doneParticle != null)
        {
            doneParticle.Play();
            AudioSource.PlayClipAtPoint(completeSound, transform.position);
        }
    }

    void FailChallenge()
    {
        challengeCompleted = false;
        isActive = false;
        actionsManager.isTimingLetters = false;
        canRepeat = true;
        letter.ResetClicks();

        Debug.Log("Challenge failed: too many wrong clicks. Try again!");
    }

    IEnumerator StopMovingAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        var rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }
    }
}

