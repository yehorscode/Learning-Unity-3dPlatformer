using TMPro;
using UnityEngine;

public class TimedLetter : MonoBehaviour
{
    public string letter;
    [SerializeField] TMP_Text letterText;
    AudioSource audioSource;
    [SerializeField] AudioClip correctSound;
    [SerializeField] AudioClip wrongSound;
    public ActionsManager actionsManager;
    int correctClicks = 0;
    int wrongClicks = 0;
    [SerializeField] TMP_Text comboText;
    [SerializeField] TMP_Text wrongText;

    void Start()
    {
        correctClicks = 0;
        wrongClicks = 0;
        audioSource = GetComponent<AudioSource>();
        GetLetter();
        if (correctSound == null || wrongSound == null)
        {
            Debug.LogError("TimedLetter: Missing audio clips! NAPRAW!!");
        }
    }

    void Update()
    {
        if (actionsManager.isTimingLetters)
        {
            Debug.Log("TimedLetter: Timing letters is active.");
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(letter.ToLower()))
                {
                    Debug.Log("TimedLetter: Correct key pressed.");
                    PlaySound(correctSound);
                    correctClicks++;
                    GetLetter();
                    comboText.text = correctClicks.ToString();
                }
                else
                {
                    Debug.Log("TimedLetter: Wrong key pressed.");
                    PlaySound(wrongSound);
                    wrongClicks++;
                    wrongText.text = wrongClicks.ToString();
                }
            }
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Audio source or clip is missing!");
        }
    }

    public void GetLetter()
    {
        string chars = "abcdefghijklmnopqrstuvwxyz";
        char c = chars[Random.Range(0, chars.Length)];
        letter = c.ToString();
        letterText.text = letter;
    }

    public (int, int) GetClicks()
    {
        return (correctClicks, wrongClicks);
    }

    public void ResetClicks()
    {
        wrongClicks = 0;
        correctClicks = 0;
    }
}

