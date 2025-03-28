using TMPro;
using UnityEngine;
public class TimedLetter : MonoBehaviour
{   
    string letter;
    [SerializeField] TMP_Text letterText;
    void Start()
    {
        GetLetter();
    }
    public void GetLetter()
    {
        string chars = "abcdefghijklmnopqrstuvwxyz";
        char c = chars[Random.Range(0, chars.Length)];
        letter = c.ToString();
        letterText.text = letter;
    }
}