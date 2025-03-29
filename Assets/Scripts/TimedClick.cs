using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimedClick : MonoBehaviour
{
    [SerializeField] Slider slider;
    public float score = 0;
    public float sliderSpeed = 50f;
    public bool isMovingRight = true;
    private bool hasScored = false;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] public ActionsManager actionsManager;
    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 100f;
        slider.value = 0f;
    }

    void Update()
    {
        if (actionsManager.isTimingClicks)
        {
            if (isMovingRight && !hasScored)
            {
                slider.value += sliderSpeed * Time.deltaTime;
                if (slider.value >= 100f)
                {
                    isMovingRight = false;
                }
            }
            else if (!isMovingRight && !hasScored)
            {
                slider.value -= sliderSpeed * Time.deltaTime;
                if (slider.value <= 0f)
                {
                    isMovingRight = true;
                }
            }

            // Check for player input
            if (Input.anyKeyDown && !hasScored)
            {
                AudioSource.PlayClipAtPoint(clickSound, transform.position);
                CalculateScore();
                hasScored = true;
                Invoke("HideCanva", 0.5f);
                                  // You might want to add logic here to reset or move to next challenge
                                  // For example: Invoke("ResetSlider", 2f);
            }
        }
    }
    void HideCanva()
    {
        actionsManager.isTimingClicks = false;
    }
    void CalculateScore()
    {
        float currentValue = slider.value;

        if (currentValue < 25f)
        {
            score = 1;
        }
        else if (currentValue >= 25f && currentValue < 35f)
        {
            score = 2;
        }
        else if (currentValue >= 35f && currentValue < 45f)
        {
            score = 3;
        }
        else if (currentValue >= 45f && currentValue < 55f)
        {
            score = 4;
        }
        else if (currentValue >= 55f && currentValue < 65f)
        {
            score = 3;
        }
        else if (currentValue >= 65f && currentValue < 75f)
        {
            score = 2;
        }
        else
        {
            score = 1;
        }

        Debug.Log("Score: " + score + " at value: " + currentValue);
    }

    public void ResetSlider()
    {
        hasScored = false;
        slider.value = 0f;
        isMovingRight = true;
    }
}
