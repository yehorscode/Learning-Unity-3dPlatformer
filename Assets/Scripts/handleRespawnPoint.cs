using UnityEngine;

public class handleRespawnPoint : MonoBehaviour
{
    PlayerManager playerManager;
    ActionsManager actionsManager;
    private void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        actionsManager = GameObject.Find("Player").GetComponent<ActionsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("RESPAWN POINT SET");
            playerManager.respawnPoint = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
            actionsManager.ShowWarnText("Respawn Point Set");
        }
    }
}
