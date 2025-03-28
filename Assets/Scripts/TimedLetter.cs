using TMPro;
using UnityEngine;
public class TimedLetter : MonoBehaviour
{   
    public string letter;
    [SerializeField] TMP_Text letterText;
    AudioSource audioSource;
    [SerializeField] AudioClip correctSound;
    [SerializeField] AudioClip wrongSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetLetter();
    }
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if(Input.GetKeyDown(letter.ToLower()))
            {
                audioSource.PlayOneShot(correctSound, transform.position.y);
                GetLetter();
            }
            else
            {
                audioSource.PlayOneShot(wrongSound, transform.position.y);
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
}
