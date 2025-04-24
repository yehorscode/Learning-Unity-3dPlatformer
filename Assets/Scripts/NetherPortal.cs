using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NetherPortal : MonoBehaviour
{
    [SerializeField] AudioClip portalEnter;
    [SerializeField] Canvas portalCanvas;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = portalEnter;
            audioSource.Play();
            portalCanvas.enabled = true;
            StartCoroutine(WaitForSound(audioSource));
        }
    }

    private IEnumerator WaitForSound(AudioSource audioSource)
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        portalCanvas.enabled = false;
        SceneManager.LoadScene("Scenes/LevelSelection");
    }
}
