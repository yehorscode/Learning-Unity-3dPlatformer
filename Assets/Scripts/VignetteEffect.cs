using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VignetteEffect : MonoBehaviour
{
    [SerializeField]public Canvas vignetteCanvas; // Reference to the Canvas
    [SerializeField] public Image vignetteImage;  // Reference to the Image component used for the vignette
    public float fadeDuration = 1f; // Duration of the fade-out effect

    void Start()
    {
        // Ensure the vignette image is hidden initially (transparent)
        vignetteImage.color = new Color(0, 0, 0, 0);
        vignetteCanvas.gameObject.SetActive(false);

    }

    public void ActivateVignette()
    {
        vignetteCanvas.gameObject.SetActive(true);
        StartCoroutine(VignetteTransition());
    }

    private IEnumerator VignetteTransition()
{
    // Show the vignette effect instantly
    vignetteImage.color = new Color(0, 0, 0, 0.6f); // Adjust alpha to control vignette intensity (0.6f is an example)

    // Wait for 0.1 seconds
    yield return new WaitForSeconds(0.1f);

    // Fade the vignette away
    float timeElapsed = 0f;
    Color initialColor = vignetteImage.color;
    Color targetColor = new Color(0, 0, 0, 0);

    while (timeElapsed < fadeDuration)
    {
        vignetteImage.color = Color.Lerp(initialColor, targetColor, timeElapsed / fadeDuration);
        timeElapsed += Time.deltaTime;
        yield return null;
    }

    // Ensure the vignette is completely transparent at the end
    vignetteImage.color = targetColor;

    // Disable the canvas after the fade-out is complete
    vignetteCanvas.gameObject.SetActive(false);
}

}
