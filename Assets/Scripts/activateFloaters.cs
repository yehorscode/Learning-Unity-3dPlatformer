using UnityEngine;

public class activateFloaters:MonoBehaviour
{
    public GameObject[] floaters;
    public void activate()
    {
        foreach (GameObject floater in floaters)
        {
            floater.GetComponent<FlyingEnemy>().isFlying = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        activate();
    }
}