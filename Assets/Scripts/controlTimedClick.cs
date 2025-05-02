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
            AudioSource.PlayClipAtPoint(doneClip, transform.position);
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
                Destroy(obj);
            }
        }
    }
}

