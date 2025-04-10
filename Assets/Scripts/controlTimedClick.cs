using System.Collections;
using UnityEngine;

public class controlTimedClick : MonoBehaviour
{
    [Header("What object to delete on correct answer")]
    [SerializeField] GameObject deletingObject;
    [SerializeField] TimedClick click;

    void OnTriggerEnter(Collider other)
    {
        ActionsManager actionsManager = other.gameObject.GetComponent<ActionsManager>();
        click.ResetSlider();
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
