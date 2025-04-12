using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VignetteEffect : MonoBehaviour
{
    [SerializeField] public Canvas vignetteCanvas;
    [SerializeField] public Image vignetteImage;
    [SerializeField] public float fadeInTime = 1f;

    void Start()
    {
        vignetteCanvas.gameObject.SetActive(false);
        vignetteImage.color = new Color(vignetteImage.color.r, vignetteImage.color.g, vignetteImage.color.b, 1f);
    }

    public void ActivateVignette()
    {
        vignetteCanvas.gameObject.SetActive(true);
        Invoke("DeactivateVignette", fadeInTime);
        StartCoroutine(FadeOut());
    }

    public void DeactivateVignette()
    {
        vignetteCanvas.gameObject.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            vignetteImage.color = new Color(vignetteImage.color.r, vignetteImage.color.g, vignetteImage.color.b, alpha);
            alpha -= Time.deltaTime / fadeInTime;
            yield return null;
        }
    }
}

