using TMPro;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{
    // Timed actions config
    [Header("Timed Actions")]
    // Timed letters
    [Header("Timed Letters")]
    [SerializeField] public bool isTimingLetters = false;
    [SerializeField] GameObject timedLettersCanvas;
    // Timed clicks (Slider)
    [Header("Timed Clicks (Slider)")]
    [SerializeField] public bool isTimingClicks = false;
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
    }
    public void ShowWarnText(string content)
    {
        infoCanvas.SetActive(true);
        warnText.text = content;
        warnTextContent = content;
        showWarnText = true;
    }
    public void HideInfoText()
    {
        infoCanvas.SetActive(false);
    }
}