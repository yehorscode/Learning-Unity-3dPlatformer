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

    // Texts
    [Header("Info canvas")]
    [SerializeField] GameObject infoCanvas;

    // Warn text
    [Header("Warn Text")]
    [SerializeField] TMP_Text warnText;
    [SerializeField] public string warnTextContent = "Empty warning text";
    [SerializeField] public bool showWarnText = false;

    void Start()
    {
        infoCanvas.SetActive(false);
    }
    void Update()
    {
        TimedActions();
        if (isTimingClicks || isTimingLetters)
        {
            isTimedEvent = true;
        }
        else if (!isTimingClicks && !isTimingLetters)
        {
            isTimedEvent = false;
        }
    }
    void TimedActions(){
        if (isTimingLetters)
        {
            timedLettersCanvas.SetActive(true);
        }
        else 
        {
            timedLettersCanvas.SetActive(false);
        }
        // Note! To reset it use ResetSlider() from TimedClick.cs
        if (isTimingClicks)
        {
            timedClicksCanvas.SetActive(true);
        }
        else {
            timedClicksCanvas.SetActive(false);
        }
    }
    public void ShowWarnText(string content)
    {
        infoCanvas.SetActive(true);
        warnText.text = content;
        warnTextContent = content;
        showWarnText = true;
        Invoke("HideInfoText", 1.5f);
    }
    public void HideInfoText()
    {
        infoCanvas.SetActive(false);
    }
}