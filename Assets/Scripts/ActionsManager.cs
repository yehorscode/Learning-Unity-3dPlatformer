using TMPro;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{
    // Timed actions config
    [Header("Timed Actions")]
    [SerializeField] public bool isTimedEvent = false;

    // Timed letters
    [Header("Timed Letters")]
    [SerializeField] public bool isTimingLetters = false;
    [SerializeField] GameObject timedLettersCanvas;

    // Timed clicks (Slider)
    [Header("Timed Clicks (Slider)")]
    [SerializeField] public bool isTimingClicks = true;
    [SerializeField] GameObject timedClicksCanvas;
    
    // Timed Target (moving image target)
    [Header("Timed Target")]
    [SerializeField] public bool isTimingTargets = false;
    [SerializeField] GameObject timedTargetCanvas;

    // Texts
    [Header("Info canvas")]
    [SerializeField] GameObject infoCanvas;

    // Warn text
    [Header("Warn Text")]
    [SerializeField] TMP_Text warnText;
    [SerializeField] public string warnTextContent = "Empty warning text";
    [SerializeField] public bool showWarnText = false;
    // Tooltip text
    [Header("Info Text")]
    [SerializeField] TMP_Text infoText;
    [SerializeField] public string infoTextContent = "Empty warning text";
    [SerializeField] public bool showInfoText = false;
    void Start()
    {
        if (timedLettersCanvas == null)
        {
            Debug.LogError("ActionsManager: Missing timedLettersCanvas! Please assign it in the inspector.");
        }
        if (timedClicksCanvas == null)
        {
            Debug.LogError("ActionsManager: Missing timedClicksCanvas! Please assign it in the inspector.");
        }
        if (timedTargetCanvas == null)
        {
            Debug.LogError("ActionsManager: Missing timedTargetCanvas! Please assign it in the inspector.");
        }
        if (infoCanvas == null)
        {
            Debug.LogError("ActionsManager: Missing infoCanvas! Please assign it in the inspector.");
        }
        if (warnText == null)
        {
            Debug.LogError("ActionsManager: Missing warnText! Please assign it in the inspector.");
        }
        if (infoText == null)
        {
            Debug.LogError("ActionsManager: Missing infoText! Please assign it in the inspector.");
        }

        infoCanvas.SetActive(false);
        infoText.text = "";
        warnText.text = "";
    }
    void Update()
    {
        TimedActions();
        if (isTimingClicks || isTimingLetters || isTimingTargets)
        {
            isTimedEvent = true;
        }
        else if (!isTimingClicks && !isTimingLetters && !isTimingTargets)
        {
            isTimedEvent = false;
        }
    }
    void TimedActions(){
        // Timing letters
        if (isTimingLetters)
        {
            timedLettersCanvas.SetActive(true);
        }
        else 
        {
            timedLettersCanvas.SetActive(false);
        }
        // Timing Clicks (slider)
        // Note! To reset it use ResetSlider() from TimedClick.cs
        if (isTimingClicks)
        {
            timedClicksCanvas.SetActive(true);
        }
        else {
            timedClicksCanvas.SetActive(false);
        }
        // Timing Targets
        if (isTimingTargets)
        {
            timedTargetCanvas.SetActive(true);
        }
        else {
            timedTargetCanvas.SetActive(false);
        }
    }
    public void ShowWarnText(string content)
    {
        infoCanvas.SetActive(true);
        warnText.text = content;
        warnTextContent = content;
        showWarnText = true;
        Invoke("HideInfoText", 1.5f);
        warnTextContent = "";
    }
    public void HideInfoText()
    {
        infoCanvas.SetActive(false);
        
    }
    public void ShowInfoText(string content)
    {
        infoCanvas.SetActive(true);
        infoText.text = content;
        infoTextContent = content;
        showWarnText = true;
        Invoke("HideInfoText", 2.5f);
        infoTextContent = "";
    }
}