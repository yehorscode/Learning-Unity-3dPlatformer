using UnityEngine;

public class challengeTimedTarget : MonoBehaviour
{
    ActionsManager actionsManager;
    TimedTarget timedTarget;
    [SerializeField] GameObject player;

    void Start()
    {
        actionsManager = player.GetComponent<ActionsManager>();
        timedTarget = player.GetComponent<TimedTarget>();
    }
    [SerializeField] int neededCorrectClicks = 5;
    [SerializeField] int maxWrongClicks = 3;
    [SerializeField] float gameDuration = 10f;
    void Update()
    {
        if (actionsManager.isTimingClicks)
        {
            if (timedTarget.HasMetClickRequirements())
            {
                CompleteChallenge();
            }
            else if (timedTarget.HasExceededWrongClicks())
            {
                FailChallenge();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            actionsManager = other.GetComponent<ActionsManager>();
            timedTarget = other.GetComponent<TimedTarget>();
            timedTarget.StartTargetClicks();
            actionsManager.isTimingTargets = true;
            timedTarget.maxWrongClicks = maxWrongClicks;
            timedTarget.neededCorrectClicks = neededCorrectClicks;
            timedTarget.gameDuration = gameDuration;
        }
    }

    void CompleteChallenge()
    {
        Debug.Log("Challenge completed!");
        actionsManager.isTimingClicks = false;
        timedTarget.EndGame();
    }

    void FailChallenge()
    {
        Debug.Log("Challenge failed: too many wrong clicks. Try again!");
        actionsManager.isTimingClicks = false;
        timedTarget.EndGame();
    }
}