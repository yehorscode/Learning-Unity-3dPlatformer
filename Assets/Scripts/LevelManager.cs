using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Scene scene;
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Scenes/Tutorial");
    }
    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Scenes/Level1");
    }
    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("Scenes/Level2");
    }
}


