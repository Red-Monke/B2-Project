using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite sprite;
    [TextArea][SerializeField] private string itemDescription;

    private PC1InventoryManager p1InventoryManager;
    private PC2InventoryManager p2InventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        p1InventoryManager = GameObject.FindGameObjectWithTag("Character1UI").GetComponent<PC1InventoryManager>();
        p2InventoryManager = GameObject.FindGameObjectWithTag("Character2UI").GetComponent<PC2InventoryManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Character1")
        {
            int leftOverItems = p1InventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            gameObject.GetComponent<Collider>().enabled = false;
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            }
        }
        else if(collision.gameObject.tag == "Character2")
        {
            int leftOverItems = p2InventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            gameObject.GetComponent<Collider>().enabled = false;
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            }
        }
    }


}
