using UnityEngine;

public class InfoTextManager : MonoBehaviour
{
    [SerializeField] ActionsManager actionsManager;
    [SerializeField] string content;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (actionsManager != null)
            {
                Debug.Log("Calling ShowInfoText with content: " + content);
                actionsManager.ShowInfoText(content);
            }
            else
            {
                Debug.LogError("actionsManager is null");
            }
        }
    }
}
