using UnityEngine;
public class controlTimedLetter : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<ActionsManager>().isTimingLetters = !other.gameObject.GetComponent<ActionsManager>().isTimingLetters;
    }
}