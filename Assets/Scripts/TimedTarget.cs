using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TimedTarget : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] float timeToMove = 2f; 
    [SerializeField] Image target; 
    [SerializeField] AudioClip hitSound; 
    [SerializeField] AudioClip missSound; 
    [SerializeField] TMP_Text scoreText; // UI element to display the score
    [SerializeField] TMP_Text timerText;
    [SerializeField] public float gameDuration = 30f; 

    private ActionsManager actionsManager;
    private AudioSource audioSource;
    private int correctClicks = 0;
    private int wrongClicks = 0;
    private float gameTimer;
    private bool gameEnded = false; // Flag to ensure EndGame is called only once

    public int neededCorrectClicks = 10;
    public int maxWrongClicks = 5;

    void Start()
    {
        ResetTargetPosition();
        actionsManager = GetComponent<ActionsManager>();
        audioSource = GetComponent<AudioSource>();
        gameTimer = gameDuration;

        if (hitSound == null || audioSource == null)
        {
            Debug.LogError("TimedTarget: Missing audio clips! Please assign them.");
        }

        // Reset target every timeToMove seconds
        InvokeRepeating(nameof(ResetTargetPosition), timeToMove, timeToMove);
    }

    void Update()
    {
        if (gameTimer > 0 && !gameEnded)
        {
            gameTimer -= Time.deltaTime;

            // Check if requirements are met or exceeded
            if (HasMetClickRequirements() || HasExceededWrongClicks())
            {
                EndGame();
                return;
            }

            if (gameTimer <= 0)
            {
                EndGame();
            }
        }

        // Update score and timer display
        if (scoreText != null && timerText != null)
        {
            scoreText.text = correctClicks.ToString();
            timerText.text = gameTimer.ToString("F2");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameTimer > 0) // Only allow clicks during the game
        {
            if (eventData.pointerCurrentRaycast.gameObject == target.gameObject)
            {
                // Increment correct clicks
                ResetTargetPosition();
                correctClicks++;

                // Play hit sound
                if (hitSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(hitSound);
                }
            }
            else
            {
                // Increment wrong clicks
                wrongClicks++;
                ResetTargetPosition();

                // Play miss sound (optional)
                if (missSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(missSound);
                }
            }
        }
    }

    void ResetTargetPosition()
    {
        if (gameTimer > 0) // Only move the target during the game
        {
            // Move target to a random position on the screen
            Vector2 randomPosition = new Vector2(
                Random.Range(30, Screen.width - target.rectTransform.rect.width - 30),
                Random.Range(30, Screen.height - target.rectTransform.rect.height - 30)
            );
            target.rectTransform.position = randomPosition;
        }
    }

    public void ResetTargetClicks()
    {
        wrongClicks = 0;
        correctClicks = 0;
    }

    public (int correct, int wrong) GetTargetClickCounts()
    {
        return (correctClicks, wrongClicks);
    }

    public void StartTargetClicks()
    {
        InvokeRepeating(nameof(ResetTargetPosition), timeToMove, timeToMove);
        ResetTargetClicks();
    }

    public bool HasMetClickRequirements()
    {
        return correctClicks >= neededCorrectClicks && wrongClicks <= maxWrongClicks;
    }

    public bool HasExceededWrongClicks()
    {
        return wrongClicks > maxWrongClicks;
    }

    public void EndGame()
    {
        if (gameEnded) return; // Prevent multiple calls

        gameEnded = true;
        CancelInvoke(nameof(ResetTargetPosition));
        Debug.Log($"Game Over! Final Score: {correctClicks} | Misses: {wrongClicks}");
        actionsManager.isTimingTargets = false;
    }
}



