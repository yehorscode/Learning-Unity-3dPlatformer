using System.Collections;
using UnityEngine;

public class controlTimedClick : MonoBehaviour
{
    [Header("What object to delete on correct answer")]
    [SerializeField] GameObject deletingObject;

    ActionsManager actionsManager;
    TimedClick click;

    private void Start()
    {
        actionsManager = GetComponent<ActionsManager>();
        click = GetComponent<TimedClick>();
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

        if (click.GetScore() > 3)
        {
            Debug.Log("Score high enough, deleting object...");
            Invoke("DeleteObject", 1f);
        }
    }

    void DeleteObject()
    {
        if (deletingObject != null)
        {
            Destroy(deletingObject);
        }
    }
}

