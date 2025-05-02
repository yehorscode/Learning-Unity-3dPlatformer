using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetherPortal : MonoBehaviour
{
    [SerializeField] AudioClip portalEnter;
    [SerializeField] GameObject portalCanvas;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (portalCanvas == null)
            {
                print("PortalCanvas is null");
                return;
            }
            print("Triggered");
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = portalEnter;
            audioSource.Play();
            portalCanvas.SetActive(true);
            StartCoroutine(WaitForSound(audioSource));
        }
    }

    private IEnumerator WaitForSound(AudioSource audioSource)
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        portalCanvas.SetActive(false);
        SceneManager.LoadScene("Scenes/LevelSelection");
    }
}
