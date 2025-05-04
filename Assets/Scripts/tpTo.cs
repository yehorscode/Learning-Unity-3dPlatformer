using UnityEngine;

public class tpTp:MonoBehaviour
{
    [SerializeField] Vector3 toWhere;
    void OnTriggerEnter(Collider other)
    {
        other.transform.position = toWhere;
    }
}
