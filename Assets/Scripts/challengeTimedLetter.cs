using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class challengeTimedLetter : MonoBehaviour
{
    [SerializeField] float neededCorrectClicks;
    [SerializeField] float maxWrongClicks;

    [Header("Option1: Deleting objects")]
    [SerializeField] List<GameObject> objectsToDelete;

    [Header("Option2: Moving objects out of the way")]
    [SerializeField] List<GameObject> objectsToMove;

    bool triggered = false;

    [Header("Player")]
    [SerializeField] ActionsManager actionsManager;
    [SerializeField] TimedLetter letter;
    float CorrectClicks;
    float WrongClicks;
    void OnTriggerEnter(Collider other)
    {
        if (triggered) return; // Prevent re-entering

        triggered = true; // Set triggered to true *before* checking conditions

        if (actionsManager == null || letter == null)
        {
            Debug.LogError("ChallengeTimedLetter: Missing required components on colliding object: " + other.gameObject.name);
            triggered = false; // Reset triggered if essential components are missing
            return;
        }

        actionsManager.isTimingLetters = true;

        // Get the current click counts *before* checking the conditions
        (int correctClicks, int wrongClicks) = letter.GetClicks();
        CorrectClicks = correctClicks;
        WrongClicks = wrongClicks;

        if (CorrectClicks >= neededCorrectClicks && WrongClicks <= maxWrongClicks)
        {
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
                        var direction = new Vector3(Random.Range(-1f, 1f), 0, 0);
                        rb.velocity = direction * Random.Range(0.5f, 1.5f);
                        StartCoroutine(StopMovingAfterDelay(obj, 15f));
                    }
                    else
                    {
                        Debug.LogError("ChallengeTimedLetter: Missing Rigidbody on object to move: " + obj.name);
                    }
                }
            }
            else
            {
                Debug.LogError("ChallengeTimedLetter: No objects to delete or move!");
            }
        }
        else
        {
            triggered = false; // Reset triggered if conditions are not met immediately
        }
    }

    IEnumerator StopMovingAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        StopMoving(obj);
    }

    void StopMoving(GameObject obj)
    {
        var rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            Debug.LogError("ChallengeTimedLetter: Missing Rigidbody on object to stop moving: " + obj.name);
        }
    }
    void Update()
    {
        if (triggered)
        {
            (int correctClicks, int wrongClicks) = letter.GetClicks();
            CorrectClicks = correctClicks;
            WrongClicks = wrongClicks;
        }        
    }
}

