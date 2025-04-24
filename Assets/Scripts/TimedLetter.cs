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
    }
    void Update()
    {
        if (actionsManager.isTimingLetters)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(letter.ToLower()))
                {
                    audioSource.PlayOneShot(correctSound, transform.position.y);
                    correctClicks++;
                    GetLetter();
                    comboText.text = correctClicks.ToString();
                }
                else
                {
                    audioSource.PlayOneShot(wrongSound, transform.position.y);
                    wrongClicks++;
                    wrongText.text = wrongClicks.ToString();
                }
            }
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

