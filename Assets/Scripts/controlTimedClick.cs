using System.Collections;
using UnityEngine;

public class controlTimedClick : MonoBehaviour
{
    [Header("What objects to delete on correct answer")]
    [SerializeField] GameObject[] deletingObjects;

    ActionsManager actionsManager;
    TimedClick click;
    [SerializeField] AudioClip doneClip;

    private void Start()
    {
        actionsManager = GetComponent<ActionsManager>();
        click = GetComponent<TimedClick>();
        gameObject.AddComponent<AudioSource>();

        if (deletingObjects == null || deletingObjects.Length == 0)
        {
            Debug.LogError("controlTimedClick: Missing deletingObjects! Please assign them in the inspector.");
        }
        if (doneClip == null)
        {
            Debug.LogError("controlTimedClick: Missing doneClip AudioClip! Please assign it in the inspector.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        click = other.GetComponent<TimedClick>();
        actionsManager = other.GetComponent<ActionsManager>();
        click?.ResetSlider();
        actionsManager.isTimingClicks = !actionsManager.isTimingClicks;
        if (click != null)
        {
            Debug.Log("Trigger entered by: " + other.name);
            StartCoroutine(WaitForScore(click));
        }
    }

    IEnumerator WaitForScore(TimedClick click)
    {
        while (!click.HasScored())
        {
            yield return null;
        }

        Debug.Log("Score is: " + click.GetScore());

        if (click.GetScore() >= 3)
        {   
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = doneClip;
            audioSource.volume = 1.5f;
            audioSource.PlayOneShot(doneClip, 1.5f);
            Debug.Log("Score high enough, deleting object...");
            Invoke("DeleteObjects", 1f);
        }
    }

    void DeleteObjects()
    {
        foreach(GameObject obj in deletingObjects)
        {
            if (obj != null)
            {
                var rb = obj.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
        StartCoroutine(DisableKinematic());
    }

    IEnumerator DisableKinematic()
    {
        yield return new WaitForSeconds(1f);
        foreach(GameObject obj in deletingObjects)
        {
            if (obj != null)
            {
                var rb = obj.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
}


