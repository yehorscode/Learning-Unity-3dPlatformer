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
    public float gameDuration; 

    private ActionsManager actionsManager;
    private AudioSource audioSource;
    public int correctClicks = 0;
    public int wrongClicks = 0;
    private float gameTimer;
    private bool gameEnded = false; // Flag to ensure EndGame is called only once

    public int neededCorrectClicks = 10;
    public int maxWrongClicks = 5;

    void Start()
    {
        gameTimer = gameDuration; // Ustawienie początkowe
        ResetTargetPosition();
        actionsManager = GetComponent<ActionsManager>();
        audioSource = GetComponent<AudioSource>();

        Debug.Log("TimedTarget: Initialization started.");

        if (hitSound == null)
        {
            Debug.LogError("TimedTarget: Missing hitSound! Please assign it in the inspector.");
        }
        if (missSound == null)
        {
            Debug.LogError("TimedTarget: Missing missSound! Please assign it in the inspector.");
        }
        if (target == null)
        {
            Debug.LogError("TimedTarget: Target Image is not assigned in the inspector.");
        }
        else
        {
            Debug.Log("TimedTarget: Target Image is assigned.");
        }
        if (scoreText == null)
        {
            Debug.LogError("TimedTarget: ScoreText is not assigned in the inspector.");
        }
        else
        {
            Debug.Log("TimedTarget: ScoreText is assigned.");
        }
        if (timerText == null)
        {
            Debug.LogError("TimedTarget: TimerText is not assigned in the inspector.");
        }
        else
        {
            Debug.Log("TimedTarget: TimerText is assigned.");
        }
        if (audioSource == null)
        {
            Debug.LogError("TimedTarget: AudioSource component is missing.");
        }
        else
        {
            Debug.Log("TimedTarget: AudioSource component is present.");
        }

        // Ensure the target has a Canvas and Graphic Raycaster for click detection
        if (target != null)
        {
            Canvas canvas = target.GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("TimedTarget: Target must be inside a Canvas for proper functionality.");
            }
            else
            {
                Debug.Log("TimedTarget: Canvas is properly assigned.");
            }

            if (canvas.renderMode != RenderMode.ScreenSpaceOverlay && canvas.worldCamera == null)
            {
                Debug.LogError("TimedTarget: The Canvas must have a worldCamera assigned if not in ScreenSpaceOverlay mode.");
            }

            GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
            if (raycaster == null)
            {
                Debug.LogError("TimedTarget: Canvas must have a Graphic Raycaster component.");
            }
            else
            {
                Debug.Log("TimedTarget: Graphic Raycaster is present.");
            }
        }

        Debug.Log("TimedTarget: Initialization completed.");

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
        if (gameTimer <= 0) // Ustaw timer tylko, jeśli gra się zakończyła
        {
            gameTimer = gameDuration;
        }
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

