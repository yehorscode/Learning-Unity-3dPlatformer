using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimedClick : MonoBehaviour
{
    [SerializeField] Slider slider;
    float score = 0;
    void Start()
    {
        slider.value = 0;
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (slider.value < 25)
            {
                score = 1;
            }
            if (slider.value > 25 && slider.value < 35)
            {
                score = 2;
            }
            if (slider.value > 35 && slider.value < 45)
            {
                score = 3;
            }
            if (slider.value > 45 && slider.value < 55)
            {
                score = 4;
            }
            if (slider.value > 55 && slider.value < 65)
            {
                score = 3;
            }
            if (slider.value > 65 && slider.value < 75)
            {
                score = 2;
            }
            if (slider.value > 75)
            {
                score = 1;
            }
        }
    }
}