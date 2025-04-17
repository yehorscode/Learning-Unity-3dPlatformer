using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimedClick : MonoBehaviour
{
    [SerializeField] Slider slider;
    public float sliderSpeed = 100f;
    public bool isMovingRight = true;
    private bool hasScored = false;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] public ActionsManager actionsManager;
    private float score = 0f;

    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 100f;
        slider.value = 0f;
    }

    void Update()
    {
        if (!actionsManager.isTimingClicks || hasScored)
        {
            return;
        }

        // Move the slider back and forth
        if (isMovingRight)
        {
            slider.value += sliderSpeed * Time.deltaTime;
            if (slider.value >= 100f)
            {
                isMovingRight = false;
            }
        }
        else
        {
            slider.value -= sliderSpeed * Time.deltaTime;
            if (slider.value <= 0f)
            {
                isMovingRight = true;
            }
        }

        // Handle player input
        if (Input.anyKeyDown)
        {
            AudioSource.PlayClipAtPoint(clickSound, transform.position);
            score = CalculateScore();
            hasScored = true;
            Invoke("HideCanvas", 0.5f);
        }
    }

    float CalculateScore()
    {
        float currentValue = slider.value;

        if (currentValue < 25f)
            return 1;
        else if (currentValue < 35f)
            return 2;
        else if (currentValue < 45f)
            return 3;
        else if (currentValue < 55f)
            return 4;
        else if (currentValue < 65f)
            return 3;
        else if (currentValue < 75f)
            return 2;
        else
            return 1;
    }

    void HideCanvas()
    {
        actionsManager.isTimingClicks = false;
    }

    public bool HasScored()
    {
        return hasScored;
    }

    public float GetScore()
    {
        return score;
    }

    public void ResetSlider()
    {
        hasScored = false;
        slider.value = 0f;
        isMovingRight = true;
    }
}
