using UnityEngine;
public class controlTimedClick : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {   
        other.gameObject.GetComponent<TimedClick>().ResetSlider();
        other.gameObject.GetComponent<ActionsManager>().isTimingClicks = !other.gameObject.GetComponent<ActionsManager>().isTimingClicks; // toggle()
    }
}
