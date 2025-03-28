using UnityEngine;

public class Inv_Collected : MonoBehaviour
{
    public string thename;
    public Sprite image;
    private Inv_Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inv_Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {                 
        inventory.AddItem(image, thename, gameObject);
    }

}
