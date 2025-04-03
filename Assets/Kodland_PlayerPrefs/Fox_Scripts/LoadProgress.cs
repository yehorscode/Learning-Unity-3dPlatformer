using UnityEngine;

public class LoadProgress : MonoBehaviour
{
    void Start()
    {
        CheckSaveFile();
    }
    void CheckSaveFile()
    {
        if (PlayerPrefs.HasKey("X") && PlayerPrefs.HasKey("Y") && PlayerPrefs.HasKey("Z") && PlayerPrefs.HasKey("Coins"))
        {
            Vector3 pos = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
            transform.position = pos;
            GetComponent<Fox_Logic>().coins = PlayerPrefs.GetInt("Coins");
        }
    }
}